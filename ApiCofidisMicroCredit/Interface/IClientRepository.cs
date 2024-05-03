using ApiCofidisMicroCredit.Model.Client;

namespace ApiCofidisMicroCredit.Interface
{
    public interface IClientRepository
    {
        public int GetClientCreditLimitDetermination(string ClientId);
        public Client? GetClientById(string ClientId);
        public string? RegisterClient(Client client);
    }
}
