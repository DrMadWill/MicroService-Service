using API.Extensions;
using DrMadWill.Extensions.Explorer;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.AppServices;

public class AppServiceController : BaseController
{
    [HttpGet("SysActions")]
    public IActionResult Index() => Ok(typeof(BaseController).GetActionExplore());

}