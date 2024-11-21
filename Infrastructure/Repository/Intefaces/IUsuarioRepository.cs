using CrombieConsole.model;

namespace Infrastructure.Repository.Intefaces
{
    public interface IUsuarioRepository
    {
        List<Estudiante> ObtenerEstudiantes();
        List<Usuario> ObtenerUsuarios();
        void RegistrarEstudiante(Estudiante estudiante);
        void RegistrarProfesor(Profesor profesor);
    }
}