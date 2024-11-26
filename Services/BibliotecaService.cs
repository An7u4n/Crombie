using CrombieConsole.model;
using Data.Repository.Intefaces;
using Services.Interfaces;


namespace CrombieConsole.Services
{
    public class BibliotecaService : IBibliotecaService
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public BibliotecaService(ILibroRepository libroRepository, IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _libroRepository = libroRepository;
        }

        public void DevolverLibro(int isbn, int idUsuario)
        {
            try
            {
                var libro = _libroRepository.ObtenerLibro(isbn);
                var usuario = _usuarioRepository.ObtenerUsuario(idUsuario);
                if (usuario == null) throw new Exception("El usuario no existe");
                if (!usuario.LibrosPrestados.Any(l => l.ISBN == libro.ISBN)) throw new Exception("El usuario no pidio prestado el libro");
                _libroRepository.AgregarHistorialDevolucion(libro, usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void PrestarLibro(int idUsuario, int isbn)
        {
            var usuario = _usuarioRepository.ObtenerUsuario(idUsuario);
            var libroAPrestar = _libroRepository.ObtenerLibro(isbn);
            if (libroAPrestar != null && libroAPrestar.Disponible == true && usuario != null)
            {
                if (usuario.LibrosPrestados.Count < usuario.LimiteLibros)
                {
                    _libroRepository.AgregarHistorialPrestamo(libroAPrestar, usuario);
                }
                else
                {
                    throw new Exception("No se presta el libro, limite alcanzado");
                }
            }
            else throw new Exception("Libro o usuario no existentes");
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            try
            {
                _libroRepository.AgregarLibro(titulo, autor, isbn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarLibro(int isbn, string titulo, string autor)
        {
            try
            {
                _libroRepository.ObtenerLibro(isbn); // Lanza excepcion si no existe el libro
                var libro = new Libro(titulo, autor, isbn);
                _libroRepository.ActualizarLibro(libro);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
