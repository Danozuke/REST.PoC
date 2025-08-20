using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PoC.DAL.Services;
using PoC.Models;
namespace PoC.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll() 
        { 
            return Ok(_usuarioService.GetAll());
        }

        [HttpGet("{usuarioId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetById(int usuarioId) 
        {
            if (usuarioId <= 0) 
            {
                ModelState.AddModelError("usuarioId", "El Id de usuario no puede ser menor, o igual que cero");
            }

            if (!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var usuario = _usuarioService.GetById(usuarioId);

            if (usuario == null) 
            { 
                return NotFound();
            }

            return Ok(usuario);

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Add([FromBody] Usuario usuario) 
        { 

            if (usuario == null)
            {
                return BadRequest();
            }

            _usuarioService.Add(usuario);
            return Created(nameof(GetById), new { usuarioId = usuario.usuarioId });
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update([FromBody] Usuario usuario)
        {
            if(usuario == null)
            {
                return BadRequest();
            }

            if(usuario.usuarioId <= 0)
            {
                ModelState.AddModelError("usuarioId", "El Id de usuario no puede ser menor, o igual que cero");
            }

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!_usuarioService.Update(usuario)) return NotFound();
            return NoContent();
            
        }

        [HttpDelete("{usuarioId}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(int usuarioId)
        {
            if (usuarioId <= 0)
            {
                ModelState.AddModelError("usuarioId", "El Id de usuario no puede ser menor, o igual que cero");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_usuarioService.Delete(usuarioId)) return NotFound();
            return NoContent();
        }


    }
}
