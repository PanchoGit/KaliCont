using Kali.Model;
using Kali.Workflow.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Kali.WebApi.Extensions;

namespace Kali.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginWorkflow workflow;

        public LoginController(ILoginWorkflow workflow)
        {
            this.workflow = workflow;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var response = workflow.Login(model);

            return this.CreateResult(response);
        }
    }
}
