using CrombieConsole.model;

namespace Data.Repository.Intefaces
{
    public interface IUsuarioRepository
    {
        ICollection<Estudiante> ObtenerEstudiantes();
        ICollection<Profesor> ObtenerProfesores();
        ICollection<Usuario> ObtenerUsuarios();
        Usuario ObtenerUsuario(int idUsuario);
        void RegistrarEstudiante(Estudiante estudiante);
        void RegistrarProfesor(Profesor profesor);
    }
}