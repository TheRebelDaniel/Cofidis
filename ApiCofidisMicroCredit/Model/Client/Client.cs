namespace ApiCofidisMicroCredit.Model.Client
{
    public class Client
    {
        public string ClientId { get; set; }
        public string FiscalNumber { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal ActualEconomicSituation { get; set; }
        public IEnumerable<Credit> CreditHistories { get; set; }
        public IEnumerable<Debt> ClientDebts { get; set; }
    }
}
