using CrombieConsole.Infrastructure.Repository;
using CrombieConsole.model;

namespace CrombieConsole.Services
{
    public class ProfesorService
    {
        private ExcelService excelService = new ExcelService("C:/Users/migue/source/repos/Crombie/CrombieConsole/BibliotecaBaseDatos.xlsx");

        public void RegistrarProfesor(string nombre, int id)
        {
            var profesor = new Profesor(nombre, id);
            excelService.AgregarProfesor(profesor);
        }
    }
}
