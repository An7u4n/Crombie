using CrombieConsole.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Requests;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly EstudianteService estudianteService;
        public EstudianteController(EstudianteService estudianteService)
        {
            this.estudianteService = estudianteService;
        }

        [HttpPost("AgregarEstudiante")]
        public IActionResult AgregarEstudiante([FromBody] AgregarEstudianteRequest request)
        {
            try
            {
                estudianteService.RegistrarEstudiante(request.Nombre, request.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo agregar el estudiante. Error: " + ex.Message);
            }
        }
    }
}
