using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;

namespace ApiCofidisMicroCredit.Repository
{
    public class RiskAnalisysRepository : IRiskAnalisysRepository
    {
        private readonly IClientRepository _clientRepository;
        public RiskAnalisysRepository(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public string GetClientRiskAnalysis(string ClientID)
        {
            //Portugal unemployment rate: 8.7%
            double unemploymentRate = 8.7;
            //Portugal inflation: 2.4%
            double inflation = 2.4;
            //Client debts
            decimal clientDebts = 0;
            //client credit history
            decimal clientCreditHistory = 0;

            //Get client by ID
            var client = _clientRepository.GetClientById(ClientID);

            if (client != null)
            {
                clientDebts = client.ClientDebts.Sum(d => d.DebtAmount);

                clientCreditHistory = client.CreditHistories.Sum(d => d.CreditAmount);
            }
            else
            {
                return string.Empty;
            }
            //Calculate Risk Index
            var riskIndex = CalculateRiskIndex(unemploymentRate, inflation, clientDebts, clientCreditHistory);

            //Returns the result of the analysis
            if (riskIndex is < 50)
            {
                return "low";
            }
            else if (riskIndex < 100)
            {
                return "medium";
            }
            else
            {
                return "high";
            }
        }

        private static double CalculateRiskIndex(double unemploymentRate, double inflation, decimal clientDebts, decimal clientCreditHistory)
        {
            //Low impact
            double unemploymentRateInpact = 0.2;
            //low impact
            double inflationInpact = 0.2;
            //moderate impact
            double clientCreditHistoryImpact = 0.4;
            //big impact
            double clientDebtsImpact = 0.6;

            // Calculate risk index
            double RiskIndex = (unemploymentRate * unemploymentRateInpact) +
                                 (inflation * inflationInpact) +
                                 (Convert.ToDouble(clientCreditHistory) / 1000 * clientCreditHistoryImpact) +
                                 (Convert.ToDouble(clientDebts) / 1000 * clientDebtsImpact);

            return RiskIndex;
        }
    }
}
