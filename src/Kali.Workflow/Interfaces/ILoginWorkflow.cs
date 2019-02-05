using Kali.Common.Domains;
using Kali.Domain;
using Kali.Model;

namespace Kali.Workflow.Interfaces
{
    public interface ILoginWorkflow
    {
        Response<UserToken> Login(LoginModel model);
    }
}
