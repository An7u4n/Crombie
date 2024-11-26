using CrombieConsole.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Services;
using Services.Interfaces;
using Web.API.Requests;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly IBibliotecaService _bibliotecaService;
        private readonly ILibroService _libroService;
        public LibroController(IBibliotecaService bibliotecaService, ILibroService libroService) 
        {
            _bibliotecaService = bibliotecaService;
            _libroService = libroService;
        }

        [HttpGet("ObtenerEstadoLibros")]
        public IActionResult GetLibros()
        {
            return Ok(_libroService.ObtenerLibros());
        }

        [HttpPost("AgregarLibro")]
        public IActionResult AgregarLibro([FromBody] LibroDTO libro)
        {
            try
            {
                _bibliotecaService.AgregarLibro(libro.Titulo, libro.Autor, libro.ISBN);
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
                _bibliotecaService.PrestarLibro(request.IdUsuario, request.ISBN);
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
                _bibliotecaService.DevolverLibro(request.ISBN, request.IdUsuario);
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
                return Ok(_libroService.ObtenerLibrosPrestadosAUsuario(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ObtenerLibro/{isbn}")]
        public IActionResult GetLibro(int isbn)
        {
            try
            {
                return Ok(_libroService.ObtenerLibro(isbn));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ActualizarLibro")]
        public IActionResult ActualizarLibro([FromBody] LibroDTO libro)
        {
            try
            {
                _bibliotecaService.ActualizarLibro(libro.ISBN, libro.Titulo, libro.Autor);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult EliminarLibro(int isbn)
        {
            try
            {
                _libroService.EliminarLibro(isbn);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
