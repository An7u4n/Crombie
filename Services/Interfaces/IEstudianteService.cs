using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface IEstudianteService
    {
        List<Estudiante> ObtenerEstudiantes();
        void RegistrarEstudiante(string nombre, int id);
    }
}