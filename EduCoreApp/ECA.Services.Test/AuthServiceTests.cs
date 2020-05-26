using ECA.Services.Implemintations;
using ECA.Services.Test.Mocks;
using ECA.Services.ViewModels;
using NUnit.Framework;

namespace ECA.Services.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AuthService_Login_Success()
        {
            AuthService authService = GetService();
            var loginModel = new LoginViewModel
            {
                Email = "john.doe@example.com",
                Password = "123456aa"
            };

            var result = authService.Login(loginModel);
            Assert.IsNotNull(result);
        }

        private static AuthService GetService()
        {
            var mockRepo = new MockUserRepository();
            var authService = new AuthService(mockRepo);
            return authService;
        }
    }
}