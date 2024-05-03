using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Login.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCofidisMicroCredit.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly IChaveMovelDigitalRepository _chaveMovelDigitalRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ITokenGeneratorRepository _tokenGeneratorRepository;


        public AuthenticationRepository(IChaveMovelDigitalRepository chaveMovelDigitalRepository, IClientRepository clientRepository, ITokenGeneratorRepository tokenGeneratorRepository)
        {
            _chaveMovelDigitalRepository = chaveMovelDigitalRepository;
            _clientRepository = clientRepository;
            _tokenGeneratorRepository = tokenGeneratorRepository;
        }


        public string LogIn(CMDRequest request, string clientId)
        {
            //Get Client information from CMD service
            var clientInformation = _chaveMovelDigitalRepository.Authenticate(request);
            if (clientInformation != null)
            {
                var client = _clientRepository.GetClientById(clientId);
                if (client != null)
                {
                    //Return authentication token
                    return _tokenGeneratorRepository.GenerateToken(clientId);
                }
            }

            //Error authenticating or user not found
            return string.Empty;
        }

        public string SignIn(CMDRequest request)
        {
            //Get Client information from CMD service
            var clientInformation = _chaveMovelDigitalRepository.Authenticate(request);

            if (clientInformation != null)
            {
                var clientId = _clientRepository.RegisterClient(clientInformation);
                if (!string.IsNullOrWhiteSpace(clientId))
                {
                    //Returns clientID
                    return clientId;
                }
            }

            //Authentication failed or client not found
            return string.Empty;
        }
    }
}
