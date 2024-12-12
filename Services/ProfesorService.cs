using Data.Repository.Excel;
using CrombieConsole.model;
using Data.Repository.Intefaces;
using Services.Interfaces;

namespace CrombieConsole.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public ProfesorService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public void RegistrarProfesor(string nombre, int id)
        {
            try
            {
                var profesor = new Profesor(nombre, id);
                _usuarioRepository.RegistrarProfesor(profesor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ICollection<Profesor> ObtenerProfesores()
        {
            try
            {
                var profesores = _usuarioRepository.ObtenerProfesores();
                if(profesores == null || profesores.Count == 0)
                    throw new Exception("No se encontraron profesores en la base de datos");
                return profesores;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
