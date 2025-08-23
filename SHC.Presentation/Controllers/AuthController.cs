using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHC.Application.Commands;
using SHC.Application.Common;
using SHC.Application.DTOs;
using SHC.Core.Domain.Patient;
using SHC.Core.Interfaces;

namespace SHC.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // This controller is responsible for handling authentication-related actions.
        // Currently, it does not contain any methods or properties.
        // You can add methods for login, registration, etc. as needed.
        // Example method for user login (to be implemented):
        private readonly IHandler<LoginCommand, Result<LoginResponseDTO>> _loginHandler;
        public AuthController(IHandler<LoginCommand, Result<LoginResponseDTO>> handler)
        {
            _loginHandler = handler;
        }

        [HttpPost("login")]
        public  async Task<IActionResult> Login(LoginCommand request)
        {
            Result<LoginResponseDTO> loginDTO = await _loginHandler.Handle(request);
            if(loginDTO.IsFailure)
            {
                return NotFound(loginDTO.Error);
            }
            return Ok(loginDTO.Value);
        }
    }
}
