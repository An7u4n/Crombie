using CrombieConsole.model;
using CrombieConsole.Model;

namespace Data.Repository.Intefaces
{
    public interface ILibroRepository
    {
        public void AgregarLibro(string titulo, string autor, int isbn);
        public List<Libro> ObtenerLibros();
        public Libro ObtenerLibro(int isbn);
        public List<HistorialBiblioteca> ObtenerHistorialBiblioteca();
        public void AgregarHistorialPrestamo(Libro libro, Usuario usuario);
        public void AgregarHistorialDevolucion(Libro libro, Usuario usuario);
        public void ActualizarDisponibilidadLibro(Libro libro);
        public List<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario);
        public void ActualizarLibro(Libro libro);
        public void EliminarLibro(Libro libro);
    }
}
