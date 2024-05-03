using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Repository;
using Moq;
using NUnit.Framework;

namespace ApiCofidisMicroCreditTests.Repository
{
    public class RiskAnalisysRepositoryTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();

        [Test]
        [TestCase(5000, 900, 0, 1000, "low")]
        [TestCase(50000, 3000, 100000, 5000, "medium")]
        [TestCase(5000, 1500, 150000, 140000, "high")]
        public void GetClientRiskAnalysis_Test(decimal actualEconomicSituation, decimal monthlyIncome, decimal debtValue, decimal creditValue, string expectedResult)
        {
            string clientId = "123";
            Client client = new()
            {
                ClientId = clientId,
                ActualEconomicSituation = actualEconomicSituation,
                ClientDebts = new List<Debt> { new Debt { DebtAmount = debtValue } },
                CreditHistories = new List<Credit> { new Credit { CreditAmount = creditValue } },
                MonthlyIncome = monthlyIncome,
                FiscalNumber = "123456789"
            };

            _clientRepositoryMock.Setup(d => d.GetClientById(clientId)).Returns(client);

            RiskAnalisysRepository riskAnalisysRepository = new RiskAnalisysRepository(_clientRepositoryMock.Object);
            var result = riskAnalisysRepository.GetClientRiskAnalysis(clientId);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetClientRiskAnalysis_ClientNotFound()
        {
            string clientId = "123";

            _clientRepositoryMock.Setup(d => d.GetClientById(clientId)).Returns((Client)null);

            RiskAnalisysRepository riskAnalisysRepository = new RiskAnalisysRepository(_clientRepositoryMock.Object);
            var result = riskAnalisysRepository.GetClientRiskAnalysis(clientId);

            Assert.IsEmpty(result);
        }
    }
}