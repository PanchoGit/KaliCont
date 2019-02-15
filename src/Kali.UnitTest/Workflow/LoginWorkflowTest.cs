//using Kali.Security;
//using Kali.Workflow;
//using Kali.Workflow.Interfaces;
//using Microsoft.Extensions.Options;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;

//namespace Kali.UnitTest.Workflow
//{
//    public class LoginWorkflowTest
//    {
//        private readonly ILoginWorkflow sut;

//        private readonly Mock<IOptions<TokenSetting>> tokenSettingMock;

//        public LoginWorkflowTest()
//        {
//            tokenSettingMock = new Mock<IOptions<TokenSetting>>();

//            sut = new LoginWorkflow(tokenSettingMock.Object);
//        }

//        [Fact]
//        public void ShouldPassLogin()
//        {
//        }
//    }
//}
