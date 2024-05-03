using ApiCofidisMicroCredit.Interface;
using ApiCofidisMicroCredit.Model.Login.Shared;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiCofidisMicroCredit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }

        [HttpPost("LogIn")]
        [Produces("application/json", "application/xml")]
        [Consumes("application/json", "application/xml")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LogIn(
            [FromBody][Required] CMDRequest request,
            [FromQuery][Required] string clientId)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(clientId))
                {
                    return BadRequest("Client login information must be provided");
                }

                //Returns authentication Token
                return Ok(_authenticationRepository.LogIn(request, clientId));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("SignIn")]
        [Produces("application/json", "application/xml")]
        [Consumes("application/json", "application/xml")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SignIn([FromBody] CMDRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Client login information must be provided");
                }

                //create user from CMD
                //Returns clientId
                return Ok(_authenticationRepository.SignIn(request));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
