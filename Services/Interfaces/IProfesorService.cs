using CrombieConsole.model;

namespace Services.Interfaces
{
    public interface IProfesorService
    {
        void RegistrarProfesor(string nombre, int id);
        public ICollection<Profesor> ObtenerProfesores();
    }
}