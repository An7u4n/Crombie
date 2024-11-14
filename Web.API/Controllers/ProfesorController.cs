using CrombieConsole.Services;
using Microsoft.AspNetCore.Mvc;
using Web.API.Requests;

namespace Web.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ProfesorService profesorService;
        public ProfesorController(ProfesorService profesorService)
        {
            this.profesorService = profesorService;
        }

        [HttpPost("AgregarProfesor")]
        public IActionResult AgregarProfesor([FromBody] AgregarProfesorRequest request)
        {
            try
            {
                profesorService.RegistrarProfesor(request.Nombre, request.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo agregar el profesor. Error: " + ex.Message);
            }
        }
    }
}
