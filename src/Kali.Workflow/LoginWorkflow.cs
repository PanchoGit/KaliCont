using Microsoft.Extensions.Options;
using Kali.Common.Domains;
using Kali.Domain;
using Kali.Model;
using Kali.Security;
using Kali.Workflow.Interfaces;
using Kali.Security.Helpers;

namespace Kali.Workflow
{
    public class LoginWorkflow : ILoginWorkflow
    {
        private const string ValidUsername = "admin";

        private const string ValidPassword = "admin";

        private readonly SecurityManager securityManager;

        public LoginWorkflow(IOptions<TokenSetting> options)
        {
            securityManager = new SecurityManager(options.Value);
        }

        public Response<UserToken> Login(LoginModel model)
        {
            var response = new Response<UserToken>();

            var validUser = GetValidUser(model);

            if (!validUser)
            {
                response.AddError("Incorrect username or password.");

                return response;
            }

            response.Data = new JwtBuilder().Create(securityManager.TokenProviderOptions);

            return response;
        }

        private bool GetValidUser(LoginModel model)
        {
            var rawPassword = CryptographyHelper.Decrypt(model.Password);

            return model.Username == ValidUsername
                   && rawPassword == ValidPassword;
        }
    }
}
