using CrombieConsole.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Services.Interfaces;
using Web.API.Requests;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet("ObtenerEstudiantes")]
        public IActionResult ObtenerEstudiantes() {
            try
            {
                var estudiantes = _estudianteService.ObtenerEstudiantes();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo obtener los estudiantes. Error: " + ex.Message);
            }
        }

        [HttpPost("AgregarEstudiante")]
        public IActionResult AgregarEstudiante([FromBody] UsuarioDTO estudiante)
        {
            try
            {
                _estudianteService.RegistrarEstudiante(estudiante.Nombre, estudiante.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo agregar el estudiante. Error: " + ex.Message);
            }
        }
    }
}
