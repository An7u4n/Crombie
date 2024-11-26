using CrombieConsole.Services;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult AgregarProfesor([FromBody] AgregarProfesorRequest request)
        {
            try
            {
                _profesorService.RegistrarProfesor(request.Nombre, request.IdUsuario);
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
