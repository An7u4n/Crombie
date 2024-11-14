using CrombieConsole.model;

namespace CrombieConsole.Services
{
    public class EstudianteService
    {
        private ExcelService excelService = new ExcelService("C:/Users/migue/source/repos/Crombie/CrombieConsole/BibliotecaBaseDatos.xlsx");

        public void RegistrarEstudiante(string nombre, int id)
        {
            var estudiante = new Estudiante(nombre, id);
            excelService.AgregarEstudiante(estudiante);
        }
    }
}
