using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface ILibroService
    {
        void DevolverLibro(int isbn, int idUsuario);
        List<Libro> ObtenerLibros();
        List<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario);
    }
}