using CrombieConsole.model;
using CrombieConsole.Model;
using Data.Repository.Intefaces;
using Services.Interfaces;

namespace Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public LibroService(ILibroRepository libroRepository, IUsuarioRepository usuarioRepository)
        {
            _libroRepository = libroRepository;
            _usuarioRepository = usuarioRepository;
        }
        public ICollection<Libro> ObtenerLibros()
        {
            return _libroRepository.ObtenerLibros();
        }

        public ICollection<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario)
        {
            try
            {
                var prestamos = _libroRepository.ObtenerHistorialBiblioteca();
                var listaISBN = new List<int>();

                foreach (var prestamo in prestamos)
                {
                    if (prestamo.Accion == Accion.Prestamo)
                    {
                        listaISBN.Add(prestamo.ISBN);
                    }
                    else if (prestamo.Accion == Accion.Devolucion)
                    {
                        listaISBN.Remove(prestamo.ISBN);
                    }
                }

                return _libroRepository.ObtenerLibros().Where(l => listaISBN.Contains(l.ISBN)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DevolverLibro(int isbn, int idUsuario)
        {
            try
            {
                var libro = _libroRepository.ObtenerLibro(isbn);
                var usuario = _usuarioRepository.ObtenerUsuario(idUsuario);
                _libroRepository.AgregarHistorialDevolucion(libro, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Libro ObtenerLibro(int isbn)
        {
            return _libroRepository.ObtenerLibros().First(l => l.ISBN == isbn);
        }

        public void EliminarLibro(int isbn)
        {
            try
            {
                var libro = _libroRepository.ObtenerLibro(isbn);
                _libroRepository.EliminarLibro(libro);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
