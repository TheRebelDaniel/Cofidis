using ApiCofidisMicroCredit.Model.Client;
using ApiCofidisMicroCredit.Model.Login.Shared;

namespace ApiCofidisMicroCredit.Interface
{
    public interface IChaveMovelDigitalRepository
    {
        public Client Authenticate(CMDRequest request);
    }
}
