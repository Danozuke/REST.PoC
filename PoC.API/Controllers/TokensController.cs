using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC.Models;
using PoC.DAL.Services;
namespace PoC.API.Controllers
{
    
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class TokensController : ControllerBase
    {
        private readonly UsuarioSistemaService _usarioSistemaService;

        public TokensController(UsuarioSistemaService usuarioSistemaService)
        {
            _usarioSistemaService = usuarioSistemaService;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] LoginData loginData)
        {
            if (loginData == null)
            {
                return BadRequest();
            }

            if(string.IsNullOrEmpty(loginData.email) || string.IsNullOrEmpty(loginData.password))
            {
                ModelState.AddModelError("Credenciales", "Email o contraseña vacía");
            }

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var usuarioSistema = _usarioSistemaService.Login(loginData.email, loginData.password);
            if (usuarioSistema != null) 
            { 
                return Ok(usuarioSistema);
            }
            else
            {
                ModelState.AddModelError("LoginError", "Error de login");
                return BadRequest(ModelState);
            }

        }
    }
}
