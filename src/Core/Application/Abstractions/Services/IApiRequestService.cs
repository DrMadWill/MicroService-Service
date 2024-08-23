using System.Diagnostics.CodeAnalysis;

namespace Application.Abstractions.Services;

public interface IApiRequestService
{
    Task<T?> Get<T>(string apiLink) where T : class;
    Task<T?> Get<T>(string apiLink, object? obj) where T : class;

    Task<T?> Post<T>(string apiLink, object? param = null)
        where T : class;

    Task<T?> Put<T>(string apiLink, object? param = null)
        where T : class;

    Task<T?> Patch<T>(string apiLink, object? param = null)
        where T : class;

    Task<T?> Delete<T>(string apiLink) where T : class;
    Task<T?> Request<T>([NotNull] Func<HttpClient, CancellationToken, HttpResponseMessage> func)
        where T : class;
}