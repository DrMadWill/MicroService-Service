using System.Diagnostics.CodeAnalysis;
using System.Text;
using Application.Abstractions.Services;
using Application.Extensions;
using DrMadWill.Layers.Repository.Extensions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;

namespace Infrastructure.Services;

public class ApiRequestService : IApiRequestService
{
    private readonly string? _token;
    private readonly string _apiBaseLink;
    public ApiRequestService(IHttpContextAccessor contextAccessor)
    {
        _token = contextAccessor.HttpContext?.Request?.Headers["Authorization"].ToString()?.Replace("Bearer", "");
        _apiBaseLink = Configuration.BaseApiLink;
    }

    
    public async Task<T?> Get<T>(string apiLink) where T : class
    {
        return await Request<T>((httpClient, token) => 
            httpClient.GetAsync(_apiBaseLink + apiLink, token).GetAwaiter().GetResult());
    }
    
    public async Task<T?> Get<T>(string apiLink,object? obj) where T : class
    {
        return await Request<T>((httpClient, token) => 
            httpClient.GetAsync(_apiBaseLink + apiLink + obj.CreateQuery(), token).GetAwaiter().GetResult());
    }


    public async Task<T?> Post<T>(string apiLink ,object? param = null ) 
        where T : class 
    {
        return await Request<T>((httpClient,token) =>
        {
            var dataString = param == null ? "" : param.JsonString();
            // create content
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");
            var response =  httpClient.PostAsync(_apiBaseLink + apiLink, content, token).GetAwaiter().GetResult();
            return response;
        });
    }
    
    
    public async Task<T?> Put<T>(string apiLink ,object? param = null ) 
        where T : class 
    {
        return await Request<T>((httpClient,token) =>
        {
            var dataString = param == null ? "" : param.JsonString();
            // create content
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");
            var response =  httpClient.PutAsync(_apiBaseLink + apiLink, content, token).GetAwaiter().GetResult();
            return response;
        });
    }
    
    public async Task<T?> Patch<T>(string apiLink ,object? param = null ) 
        where T : class 
    {
        return await Request<T>((httpClient,token) =>
        {
            var dataString = param == null ? "" : param.JsonString();
            // create content
            var content = new StringContent(dataString, Encoding.UTF8, "application/json");
            var response =  httpClient.PatchAsync(_apiBaseLink + apiLink, content, token).GetAwaiter().GetResult();
            return response;
        });
    }
    
    public async Task<T?> Delete<T>(string apiLink) where T : class
    {
        return await Request<T>((httpClient, token) => 
            httpClient.DeleteAsync(_apiBaseLink + apiLink, token).GetAwaiter().GetResult());
    }
    
    
    public async Task<T?> Request<T>([NotNull]Func<HttpClient,CancellationToken,HttpResponseMessage> func) 
        where T : class 
    {
        try
        {
            using var httpClient = new HttpClient();
            // default json
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(_token))
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
       
            using var cancellationToken = new CancellationTokenSource(new TimeSpan(0, 5, 0));
            var response = func(httpClient,cancellationToken.Token);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.JsonString());
                return null;
            }

            var str = await response.Content.ReadAsStringAsync(cancellationToken.Token);
            // content read
            return JsonConvert.DeserializeObject<T>(str);;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Log.Error("ApiRequestService | Error | {error}",e);
        }
        return null;
    }
    
    
    
    
}

public static class RequestHelp
{
    public static string CreateQuery(this object? obj)
    {
        if (obj is null) return string.Empty;
    
        var properties = obj.GetType().GetProperties()
            .Where(p => p.GetValue(obj) != null) // Null değerleri göz ardı et
            .Where(p => 
                p.PropertyType.IsPrimitive || // İlkel tipleri dikkate al
                p.PropertyType.Equals(typeof(string)) || // String tipleri dikkate al
                p.PropertyType.IsValueType) // Değer tiplerini (structs) dikkate al
            .Where(p => !p.PropertyType.Equals(typeof(object))) // Tipi object olanları çıkar
            .Select(p => 
                Uri.EscapeDataString(p.Name) + "=" + Uri.EscapeDataString(p.GetValue(obj)?.ToString() ?? string.Empty)); // URL kodlaması
        var objProperties = obj.GetType().GetProperties()
            .Where(p => p.GetValue(obj) != null) // Null değerleri göz ardı et
            .Where(p => p.PropertyType.IsClass ||
                        p.PropertyType == typeof(object)) // Sadece class ve object tipindekileri seç
            .Select(p => p.GetType().IsArray ? "" 
                    : p.GetValue(obj).CreateQuery(Uri.EscapeDataString(p.Name)));

        var allProps = properties.Concat(objProperties);

        return "?" + string.Join("&", allProps);
    }

    public static string CreateQuery(this object? obj, string name)
    {
        if (obj is null) return string.Empty;

        var properties = obj.GetType().GetProperties()
            .Where(p => p.GetValue(obj) != null) // Null değerleri göz ardı et
            .Where(p =>
                p.PropertyType.IsPrimitive || // İlkel tipleri dikkate al
                p.PropertyType.Equals(typeof(string)) || // String tipleri dikkate al
                p.PropertyType.IsValueType) // Değer tiplerini (structs) dikkate al
            .Where(p => !p.PropertyType.Equals(typeof(object))) // Tipi object olanları çıkar
            .Select(p => name + "." +
                         Uri.EscapeDataString(p.Name) + "=" +
                         Uri.EscapeDataString(p.GetValue(obj)?.ToString() ?? string.Empty)); // URL kodlaması


        return string.Join("&", properties);
    }
}
