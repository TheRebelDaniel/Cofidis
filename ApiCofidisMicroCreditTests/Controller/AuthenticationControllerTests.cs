using ApiCofidisMicroCredit.Controllers;
using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Login.Shared;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Controller
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock = new();
        private readonly Fixture _fixture = new();

        [Test]
        public void LogIn_Success()
        {
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.LogIn(_fixture.Create<CMDRequest>(), _fixture.Create<string>());

            Assert.That(typeof(OkObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void LogIn_BadRequest()
        {
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.LogIn(_fixture.Create<CMDRequest>(), string.Empty);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void LogIn_BadRequest_2()
        {
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.LogIn(null, _fixture.Create<string>());

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void LogIn_ThrowsException()
        {
            _authenticationRepositoryMock.Setup(d => d.LogIn(It.IsAny<CMDRequest>(), It.IsAny<string>())).Throws(new Exception());
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.LogIn(_fixture.Create<CMDRequest>(), _fixture.Create<string>());

            Assert.That(typeof(ObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void SignIn_Success()
        {
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.SignIn(_fixture.Create<CMDRequest>());

            Assert.That(typeof(OkObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void SignIn_BadRequest()
        {
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.SignIn(null);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void SignIn_ThrowsException()
        {
            _authenticationRepositoryMock.Setup(d => d.SignIn(It.IsAny<CMDRequest>())).Throws(new Exception());
            AuthenticationController authenticationController = new AuthenticationController(_authenticationRepositoryMock.Object);

            var result = authenticationController.SignIn(_fixture.Create<CMDRequest>());

            Assert.That(typeof(ObjectResult), Is.EqualTo(result.GetType()));
        }
    }
}
