using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Repository;
using AutoFixture;
using Moq;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Repository
{
    public class ClientRepositoryTests
    {
        private readonly Mock<IDatabaseRepository> _databaseRepositoryMock = new();
        private readonly Fixture _fixture = new();

        [Test]
        public void CallSpCreditLimitDetermination_Sucess()
        {
            Client client = _fixture.Create<Client>();

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<int>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<int> { 0 });

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.GetClientCreditLimitDetermination(client.ClientId);

            Assert.That(0, Is.EqualTo(result));
        }
        [Test]
        public void CallSpCreditLimitDetermination_ReturnsEmpty()
        {
            Client client = _fixture.Create<Client>();

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<int>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<int>());

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.GetClientCreditLimitDetermination(client.ClientId);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void CallSpGetClientById_Sucess()
        {
            Client client = _fixture.Create<Client>();

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<Client>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<Client> { client });

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.GetClientById(client.ClientId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.GetType(), Is.EqualTo(typeof(Client)));
        }

        [Test]
        public void CallSpGetClientById_ReturnsEmpty()
        {
            Client client = _fixture.Create<Client>();

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<Client>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<Client>());

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.GetClientById(client.ClientId);

            Assert.That(result, Is.Null);
        }

        [Test]
        public void RegisterClient_Success()
        {
            Client client = new Client
            {
                ClientId = _fixture.Create<string>(),
                ActualEconomicSituation = 10000,
                ClientDebts = new List<Debt> {
                    new Debt {
                        DebtAmount = 1000,
                        DebtDateTime = DateTime.UtcNow
                    }
                },
                CreditHistories = new List<Credit>
                {
                    new Credit
                    {
                        CreditAmount = 1000,
                        CreditDateTime= DateTime.UtcNow
                    }
                },
                FiscalNumber = "123456789",
                MonthlyIncome = 1500
            };

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<string>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<string> { client.ClientId });

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.RegisterClient(client);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void RegisterClient_RegistrationFails()
        {
            Client client = new Client
            {
                ClientId = _fixture.Create<string>(),
                ActualEconomicSituation = 10000,
                ClientDebts = new List<Debt> {
                    new Debt {
                        DebtAmount = 1000,
                        DebtDateTime = DateTime.UtcNow
                    }
                },
                CreditHistories = new List<Credit>
                {
                    new Credit
                    {
                        CreditAmount = 1000,
                        CreditDateTime= DateTime.UtcNow
                    }
                },
                FiscalNumber = "123456789",
                MonthlyIncome = 1500
            };

            _databaseRepositoryMock.Setup(d => d.ExecuteStoredProcedure<string>(It.IsAny<string>(), It.IsAny<object>())).Returns(new List<string>());

            ClientRepository clientRepository = new(_databaseRepositoryMock.Object);

            var result = clientRepository.RegisterClient(client);

            Assert.That(result, Is.Null);
        }
    }
}
