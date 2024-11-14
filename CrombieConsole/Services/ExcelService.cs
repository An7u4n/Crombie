using CrombieConsole.Infrastructure.Repository;
using CrombieConsole.model;
using CrombieConsole.Model;

namespace CrombieConsole.Services
{
    public class ExcelService
    {
        public LibroExcelRepository LibroRepository { get; set; }
        public UsuarioExcelResporitory UsuarioRepository { get; set; }
        public ExcelService(string filepath) 
        {
            LibroRepository = new LibroExcelRepository(filepath);
            UsuarioRepository = new UsuarioExcelResporitory(filepath);
        }

        public void CargarDatosExcel(Biblioteca biblioteca)
        {
            var libros = LibroRepository.ObtenerLibros();

            foreach (var libro in libros)
            {
                biblioteca.Libros.Add(libro);
            }

            var usuarios = UsuarioRepository.ObtenerUsuarios();

            foreach (var usuario in usuarios)
            {
                biblioteca.Usuarios.Add(usuario);
            }

            var prestamos = LibroRepository.ObtenerHistorialBiblioteca();

            foreach (var prestamo in prestamos)
            {
                var libro = biblioteca.Libros.FirstOrDefault(l => l.ISBN == prestamo.ISBN);
                var usuario = biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == prestamo.IdUsuario);
                if (usuario != null && libro != null)
                {
                    if (prestamo.Accion == Accion.Prestamo)
                    {
                        usuario.LibrosPrestados.Add(libro);
                        libro.Disponible = false;
                    } else if (prestamo.Accion == Accion.Devolucion)
                    {
                        usuario.LibrosPrestados.Remove(libro);
                        libro.Disponible = true;
                    }
                }
            }
        }

        public void AgregarHistorialPrestamo(Libro libro, Usuario usuario)
        {
            LibroRepository.AgregarHistorialPrestamo(libro, usuario);
        }

        public void AgregarHistorialDevolucion(Libro libro, Usuario usuario)
        {
            LibroRepository.AgregarHistorialDevolucion(libro, usuario);
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            LibroRepository.AgregarLibro(titulo, autor, isbn);
        }
    }
}
