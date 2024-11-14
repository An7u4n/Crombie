using ClosedXML.Excel;
using CrombieConsole.model;

namespace CrombieConsole.Infrastructure.Repository
{
    public class UsuarioExcelResporitory
    {
        public string Filepath { get; set; }

        public UsuarioExcelResporitory(string filepath)
        {
            Filepath = filepath;
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
