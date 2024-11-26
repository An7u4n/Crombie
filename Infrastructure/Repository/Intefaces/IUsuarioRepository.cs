using CrombieConsole.model;

namespace Data.Repository.Intefaces
{
    public interface IUsuarioRepository
    {
        List<Estudiante> ObtenerEstudiantes();
        List<Profesor> ObtenerProfesores();
        List<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuario(int idUsuario);
        void RegistrarEstudiante(Estudiante estudiante);
        void RegistrarProfesor(Profesor profesor);
    }
}