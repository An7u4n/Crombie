using ClosedXML.Excel;
using CrombieConsole.model;

namespace CrombieConsole.Infrastructure.Repository
{
    public class UsuarioExcelRepository
    {
        public string Filepath { get; set; }

        public UsuarioExcelRepository(string filepath)
        {
            Filepath = filepath;
        }

        public void RegistrarProfesor(Profesor profesor)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                worksheet.Cell(lastRowUsed + 1, 1).Value = profesor.IdUsuario;
                worksheet.Cell(lastRowUsed + 1, 2).Value = profesor.Nombre;
                worksheet.Cell(lastRowUsed + 1, 3).Value = "Profesor";

                workbook.Save();
            }
        }

        public void RegistrarEstudiante(Estudiante estudiante)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                worksheet.Cell(lastRowUsed + 1, 1).Value = estudiante.IdUsuario;
                worksheet.Cell(lastRowUsed + 1, 2).Value = estudiante.Nombre;
                worksheet.Cell(lastRowUsed + 1, 3).Value = "Estudiante";

                workbook.Save();
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            var dataList = new List<Usuario>();

            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    if(worksheet.Cell(row, 3).GetValue<string>() == "Estudiante")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        dataList.Add(new Estudiante(nombre, idUsuario));
                    }else if(worksheet.Cell(row, 3).GetValue<string>() == "Profesor")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        dataList.Add(new Profesor(nombre, idUsuario));
                    }
                }
            }

            return dataList;
        }
    }
}
