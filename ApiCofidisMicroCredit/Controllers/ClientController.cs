using ApiCofidisMicroCredit.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ApiCofidisMicroCredit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IRiskAnalisysRepository _riskAnalisysRepository;

        public ClientController(IClientRepository clientRepository, IRiskAnalisysRepository riskAnalisysRepository)
        {
            _clientRepository = clientRepository;
            _riskAnalisysRepository = riskAnalisysRepository;
        }

        [HttpGet("GetClientCreditLimit")]
        [Produces("application/json", "application/xml")]
        [Consumes("application/json", "application/xml")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetClientCreditLimit([FromQuery][Required] string clientId)
        {
            try
            {
                if (clientId.IsNullOrEmpty())
                {
                    return BadRequest("clientId must be provided");
                }

                //validate if client exists
                var client = _clientRepository.GetClientById(clientId);

                if (client == null)
                {
                    return BadRequest("Client does not exists");
                }

                //Call Credit limit Determination
                var result = _clientRepository.GetClientCreditLimitDetermination(client.ClientId);

                if (result.Equals(0))
                {
                    return BadRequest("Error analyzing client's credit limit");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetClientRiskAnalisys")]
        [Produces("application/json", "application/xml")]
        [Consumes("application/json", "application/xml")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetClientRiskAnalisys([FromQuery][Required] string clientId)
        {
            try
            {
                if (clientId.IsNullOrEmpty())
                {
                    return BadRequest("clientId must be provided");
                }

                //validate if client exists
                var client = _clientRepository.GetClientById(clientId);

                if (client == null)
                {
                    return BadRequest("Client does not exists");
                }

                string riskAnalisys = _riskAnalisysRepository.GetClientRiskAnalysis(clientId);

                if (!riskAnalisys.IsNullOrEmpty())
                {
                    return Ok(riskAnalisys);
                }
                else
                {
                    return BadRequest("Error while analyzing the client's risk.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
