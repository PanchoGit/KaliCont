using Microsoft.AspNetCore.Mvc.Filters;
using Kali.WebApi.Helpers;

namespace Kali.WebApi.Filters
{
    public class VersionHeaderFilter : ActionFilterAttribute
    {
        public const string HeaderAppVersionName = "x-app-version";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var response = context.HttpContext.Response;

            response?.Headers.Add(HeaderAppVersionName, VersionHelper.Version);
        }
    }
}
