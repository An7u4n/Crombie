using CrombieConsole.model;
using CrombieConsole.Model;

namespace Infrastructure.Repository.Intefaces
{
    public interface ILibroRepository
    {
        public void AgregarLibro(string titulo, string autor, int isbn);
        public List<Libro> ObtenerLibros();
        public List<HistorialBiblioteca> ObtenerHistorialBiblioteca();
        public void AgregarHistorialPrestamo(Libro libro, Usuario usuario);
        public void AgregarHistorialDevolucion(Libro libro, Usuario usuario);
        public void ActualizarDisponibilidadLibro(Libro libro);
    }
}
