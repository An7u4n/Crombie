using CrombieConsole.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.API.Requests;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly BibliotecaService bibliotecaService;
        public LibroController(BibliotecaService bibliotecaService) 
        {
            this.bibliotecaService = bibliotecaService;
        }

        [HttpGet("ObtenerEstadoLibros")]
        public IActionResult GetLibros()
        {
            return Ok(bibliotecaService.ObtenerLibros());
        }

        [HttpPost("AgregarLibro")]
        public IActionResult AgregarLibro([FromBody] AgregarLibroRequest request)
        {
            try
            {
                bibliotecaService.AgregarLibro(request.Titulo, request.Autor, request.ISBN);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo agregar el libro. Error: "+ex.Message);
            }
        }

        [HttpPost("PrestarLibro")]
        public IActionResult PrestarLibro([FromBody] PrestarLibroRequest request)
        {
            try
            {
                bibliotecaService.PrestarLibro(request.IdUsuario, request.ISBN);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DevolverLibro")]
        public IActionResult DevolverLibro([FromBody] PrestarLibroRequest request)
        {
            try
            {
                bibliotecaService.DevolverLibro(request.ISBN, request.IdUsuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerLibrosPrestadosAUsuario/{id}")]
        public IActionResult GetLibrosPrestadosAUsuario(int id)
        {
            try
            {
                return Ok(bibliotecaService.LibrosPrestadosUsuario(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
