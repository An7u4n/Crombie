using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface ILibroService
    {
        void DevolverLibro(int isbn, int idUsuario);
        ICollection<Libro> ObtenerLibros();
        Libro ObtenerLibro(int isbn);
        ICollection<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario);
        void EliminarLibro(int isbn);
    }
}