using CrombieConsole.Services;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Services.Interfaces;
using Web.API.Requests;

namespace Web.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;
        public ProfesorController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpPost("AgregarProfesor")]
        public IActionResult AgregarProfesor([FromBody] UsuarioDTO profesor)
        {
            try
            {
                _profesorService.RegistrarProfesor(profesor.Nombre, profesor.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo agregar el profesor. Error: " + ex.Message);
            }
        }

        [HttpGet("ObtenerProfesores")]
        public IActionResult ObtenerProfesores()
        {
            try
            {
                var profesores = _profesorService.ObtenerProfesores();
                return Ok(profesores);
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener los profesores. Error: " + ex.Message);
            }
        }
    }
}
