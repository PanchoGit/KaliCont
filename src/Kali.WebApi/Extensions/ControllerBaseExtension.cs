using Kali.Common.Domains;
using Microsoft.AspNetCore.Mvc;

namespace Kali.WebApi.Extensions
{
    public static class ControllerBaseExtension
    {
        public static IActionResult CreateResult(this ControllerBase controller, Response response)
        {
            if (response.HasErrors)
                return controller.BadRequest(response);

            return controller.Ok(response);
        }
    }
}
