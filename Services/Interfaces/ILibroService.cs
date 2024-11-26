using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface ILibroService
    {
        void DevolverLibro(int isbn, int idUsuario);
        List<Libro> ObtenerLibros();
        Libro ObtenerLibro(int isbn);
        List<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario);
    }
}