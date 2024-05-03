using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Client;
using System.Data;

namespace ApiCofidisMicroCredit.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ClientRepository(IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public int GetClientCreditLimitDetermination(string ClientId)
        {
            var storedProcedureResult = _databaseRepository.ExecuteStoredProcedure<int>("sp_ClientCreditLimitDetermination", new { ClientID = ClientId });
            if (storedProcedureResult.Any())
            {
                return storedProcedureResult.FirstOrDefault();
            }
            else
            {
                return 0;
            }
        }

        public Client? GetClientById(string ClientId)
        {
            var storedProcedureResult = _databaseRepository.ExecuteStoredProcedure<Client>("sp_GetClientById", new { ClientID = ClientId });
            if (storedProcedureResult.Any())
            {
                return storedProcedureResult.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        public string? RegisterClient(Client client)
        {
            //Create credit history data table
            DataTable creditHistories = new();
            creditHistories.Columns.Add("CreditAmount", typeof(decimal));
            //Map parameter
            foreach (var credit in client.CreditHistories)
            {
                creditHistories.Rows.Add(credit.CreditAmount);
            }

            //Create debts data table
            DataTable clientDebts = new DataTable();
            clientDebts.Columns.Add("DebtAmount", typeof(decimal));
            //Map parameter
            foreach (var debt in client.ClientDebts)
            {
                clientDebts.Rows.Add(debt.DebtAmount);
            }
            //SQL parameters for the stored procedure
            object sqlParameters = new
            {
                FiscalNumber = client.FiscalNumber,
                MonthlyIncome = client.MonthlyIncome,
                ActualEconomicSituation = client.ActualEconomicSituation,
                CreditHistories = creditHistories,
                ClientDebts = clientDebts
            };

            //Call stored procedure
            var storedProcedureResult = _databaseRepository.ExecuteStoredProcedure<string>("sp_RegisterClient", sqlParameters);
            if (storedProcedureResult.Any())
            {
                return storedProcedureResult.FirstOrDefault();
            }
            else
            {
                return null;
            }
        }
    }
}
