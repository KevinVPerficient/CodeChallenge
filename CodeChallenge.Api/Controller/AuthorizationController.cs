using CodeChallenge.Business.DTOs;
using CodeChallenge.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        public AuthorizationController(IAuthorizationService AuthorizationService)
        {
            _authorizationService = AuthorizationService;
        }

        [HttpPost("Authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Authorize(AuthDto userDto)
        {
            var validAccess = _authorizationService.ValidateAccess(userDto);
            if (!validAccess) return BadRequest(new { message = "Wrong credentials" });

            var token = _authorizationService.GenerateToken(userDto);

            return Ok(new { token = token });
        }
    }
}
