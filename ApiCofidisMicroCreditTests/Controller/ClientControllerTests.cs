using ApiCofidisMicroCredit.Controllers;
using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Controller
{
    public class ClientControllerTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<IRiskAnalisysRepository> _riskAnalisysRepositoryMock = new();
        private readonly Fixture fixture = new();

        [Test]
        public void GetClientCreditLimit_Success()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns(client);
            _clientRepositoryMock.Setup(d => d.GetClientCreditLimitDetermination(client.ClientId)).Returns(1000);

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientCreditLimit(client.ClientId);

            Assert.That(typeof(OkObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientCreditLimit_ClientId_Empty()
        {
            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientCreditLimit(string.Empty);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientCreditLimit_ClientNotExists()
        {
            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientCreditLimit("123");

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientCreditLimit_CreditLimitError()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns(client);
            _clientRepositoryMock.Setup(d => d.GetClientCreditLimitDetermination(client.ClientId)).Returns(0);

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientCreditLimit(client.ClientId);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientCreditLimit_ThrowsException()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Throws(new Exception());

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientCreditLimit(client.ClientId);

            Assert.That(typeof(ObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientRiskAnalisys_Success()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns(client);
            _riskAnalisysRepositoryMock.Setup(d => d.GetClientRiskAnalysis(client.ClientId)).Returns("high");

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientRiskAnalisys(client.ClientId);

            Assert.That(typeof(OkObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientRiskAnalisys_ThrowsException()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Throws(new Exception());

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientRiskAnalisys(client.ClientId);

            Assert.That(typeof(ObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientRiskAnalisys_AnalisysError()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns(client);
            _riskAnalisysRepositoryMock.Setup(d => d.GetClientRiskAnalysis(client.ClientId)).Returns(string.Empty);

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientRiskAnalisys(client.ClientId);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientRiskAnalisys_ClientNotFound()
        {
            var client = fixture.Create<Client>();
            _clientRepositoryMock.Setup(d => d.GetClientById(client.ClientId)).Returns((Client)null);

            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientRiskAnalisys(client.ClientId);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }

        [Test]
        public void GetClientRiskAnalisys_BadRequest()
        {
            ClientController clientController = new(_clientRepositoryMock.Object, _riskAnalisysRepositoryMock.Object);

            var result = clientController.GetClientRiskAnalisys(string.Empty);

            Assert.That(typeof(BadRequestObjectResult), Is.EqualTo(result.GetType()));
        }
    }
}
