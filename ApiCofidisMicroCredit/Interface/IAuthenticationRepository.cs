using ApiCofidisMicroCredit.Model.Login.Shared;
using System.IdentityModel.Tokens.Jwt;

namespace ApiCofidisMicroCredit.Interface
{
    public interface IAuthenticationRepository
    {
        public string LogIn(CMDRequest request, string clientId);
        public string SignIn(CMDRequest request);
    }
}
