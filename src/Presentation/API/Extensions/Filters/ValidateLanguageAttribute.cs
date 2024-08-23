using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Extensions.Filters;

// public class ValidateLanguageAttribute : ActionFilterAttribute
// {
//     public override void OnActionExecuting(ActionExecutingContext context)
//     {
//         var language = context.HttpContext.Request.Headers["x-lang"].FirstOrDefault();
//
//         if (!string.IsNullOrEmpty(language) && !IsLanguageValid(language))
//         {
//             context.Result = new BadRequestObjectResult("Invalid language type.");
//         }
//
//         base.OnActionExecuting(context);
//     }
//
//     private bool IsLanguageValid(string language) => LangConstant.Langs.Contains(language);
// }