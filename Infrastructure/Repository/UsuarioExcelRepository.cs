using ClosedXML.Excel;
using CrombieConsole.model;
using DocumentFormat.OpenXml.Drawing;
using Data.Repository.Intefaces;

namespace CrombieConsole.Data.Repository
{
    public class UsuarioExcelRepository : IUsuarioRepository
    {
        public string Filepath { get; set; }
        private readonly ILibroRepository _libroRepository;

        public UsuarioExcelRepository(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
            Filepath = "C:/Users/migue/source/repos/Crombie/CrombieConsole/BibliotecaBaseDatos.xlsx";
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
                    if (worksheet.Cell(row, 3).GetValue<string>() == "Estudiante")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        var estudiante = new Estudiante(nombre, idUsuario);
                        estudiante.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                        dataList.Add(estudiante);
                    }
                    else if (worksheet.Cell(row, 3).GetValue<string>() == "Profesor")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        var profesor = new Profesor(nombre, idUsuario);
                        profesor.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                        dataList.Add(profesor);
                    }
                }
            }

            return dataList;
        }

        public List<Estudiante> ObtenerEstudiantes()
        {
            var dataList = new List<Estudiante>();

            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    if (worksheet.Cell(row, 3).GetValue<string>() == "Estudiante")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        var estudiante = new Estudiante(nombre, idUsuario);
                        estudiante.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                        dataList.Add(estudiante);
                    }
                }
            }

            return dataList;
        }

        public List<Profesor> ObtenerProfesores()
        {
            var dataList = new List<Profesor>();

            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    if (worksheet.Cell(row, 3).GetValue<string>() == "Profesor")
                    {
                        var idUsuario = worksheet.Cell(row, 1).GetValue<int>();
                        var nombre = worksheet.Cell(row, 2).GetValue<string>();
                        var profesor = new Profesor(nombre, idUsuario);
                        profesor.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                        dataList.Add(profesor);
                    }
                }
            }
            return dataList;
        }
        public Usuario ObtenerUsuario(int idUsuario)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los usuarios es 1
                var worksheet = workbook.Worksheet(1);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    var idUsuarioExcel = worksheet.Cell(row, 1).GetValue<int>();
                    if (idUsuarioExcel == idUsuario)
                    {
                        if (worksheet.Cell(row, 3).GetValue<string>() == "Estudiante")
                        {
                            var nombre = worksheet.Cell(row, 2).GetValue<string>();
                            var usuario = new Estudiante(nombre, idUsuario);
                            usuario.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                            return usuario;
                        }
                        else if (worksheet.Cell(row, 3).GetValue<string>() == "Profesor")
                        {
                            var nombre = worksheet.Cell(row, 2).GetValue<string>();
                            var usuario = new Profesor(nombre, idUsuario);
                            usuario.LibrosPrestados = _libroRepository.ObtenerLibrosPrestadosAUsuario(idUsuario);
                            return usuario;
                        }
                    }
                }
            }
            throw new Exception("Usuario no encontrado");
        }
    }
}
