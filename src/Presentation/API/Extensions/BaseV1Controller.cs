using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class BaseV1Controller : BaseController
{



   
}