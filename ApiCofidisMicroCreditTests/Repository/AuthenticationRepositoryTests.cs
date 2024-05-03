using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Model.Login.Shared;
using ApiCofidisMicroCredit.Repository;
using AutoFixture;
using Moq;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Repository
{
    public class AuthenticationRepositoryTests
    {
        private readonly Mock<IChaveMovelDigitalRepository> _chaveMovelDigitalRepositoryMock = new();
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<ITokenGeneratorRepository> _tokenRepositoryMock = new();
        private readonly Fixture fixture = new();

        [Test]
        public void LogIn_Success()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns(client);
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns(client);
            _tokenRepositoryMock.Setup(d => d.GenerateToken(client.ClientId)).Returns(fixture.Create<string>());

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.LogIn(cmdRequest, client.ClientId);

            Assert.IsNotNull(result);
        }

        [Test]
        public void LogIn_ClientDontExist()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns(client);
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns((Client)null);

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.LogIn(cmdRequest, client.ClientId);

            Assert.IsEmpty(result);
        }

        [Test]
        public void LogIn_AuthenticationFailed()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns((Client)null);

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.LogIn(cmdRequest, client.ClientId);

            Assert.IsEmpty(result);
        }


        [Test]
        public void SignIn_Success()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns(client);
            _clientRepositoryMock.Setup(d => d.RegisterClient(client)).Returns(client.ClientId);

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.SignIn(cmdRequest);

            Assert.IsNotNull(result);
        }

        [Test]
        public void SignIn_AuthenticationFail()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns((Client)null);
            //_clientRepositoryMock.Setup(d => d.RegisterClient(client)).Returns(client.ClientId);

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.SignIn(cmdRequest);

            Assert.IsEmpty(result);
        }

        [Test]
        public void SignIn_FailToSignIn()
        {
            var cmdRequest = fixture.Create<CMDRequest>();
            var client = fixture.Create<Client>();
            client.ClientId = "123456789";

            _chaveMovelDigitalRepositoryMock.Setup(d => d.Authenticate(cmdRequest)).Returns(client);
            _clientRepositoryMock.Setup(d => d.RegisterClient(client)).Returns(string.Empty);

            AuthenticationRepository repo = new(_chaveMovelDigitalRepositoryMock.Object, _clientRepositoryMock.Object, _tokenRepositoryMock.Object);

            var result = repo.SignIn(cmdRequest);

            Assert.IsEmpty(result);
        }
    }
}
