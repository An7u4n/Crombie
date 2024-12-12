using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface IEstudianteService
    {
        ICollection<Estudiante> ObtenerEstudiantes();
        void RegistrarEstudiante(string nombre, int id);
    }
}