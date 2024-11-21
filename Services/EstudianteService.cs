using CrombieConsole.model;

namespace CrombieConsole.Services
{
    public class EstudianteService
    {
        private ExcelService excelService = new ExcelService();

        public void RegistrarEstudiante(string nombre, int id)
        {
            var estudiante = new Estudiante(nombre, id);
            excelService.AgregarEstudiante(estudiante);
        }

        public List<Estudiante> ObtenerEstudiantes()
        {
            return excelService.ObtenerEstudiantes();
        }
    }
}
