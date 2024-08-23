using System.Net;
using DrMadWill.Extensions.Response;
using DrMadWill.Layers.Core;
using DrMadWill.Layers.Core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
    
    
    [NonAction]
    protected T WriteLang<T>(T req)
        where T : class,ILang
    {
        var lang = (HttpContext.Request?.Headers["x-lang"])?.ToString() ?? "az";
        req.Lang = !string.IsNullOrWhiteSpace(lang) && !string.IsNullOrEmpty(lang) && lang != "" ?  lang : "az"  ;
        return req;
    }
    
    protected IActionResult Result(bool result,string successMessage,string filedMessage)
    {
        return result ? Ok( GenericResponse<string>.Succeed(successMessage) ) : 
            StatusCode((int)HttpStatusCode.BadRequest,SysResponse.Failed(filedMessage)); 
    }
}