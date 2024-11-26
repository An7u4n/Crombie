using CrombieConsole.model;
using Data.Repository.Intefaces;
using Services.Interfaces;

namespace CrombieConsole.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public EstudianteService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void RegistrarEstudiante(string nombre, int id)
        {
            var estudiante = new Estudiante(nombre, id);
            try
            {
                _usuarioRepository.RegistrarEstudiante(estudiante);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Estudiante> ObtenerEstudiantes()
        {
            try
            {
                return _usuarioRepository.ObtenerEstudiantes();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
