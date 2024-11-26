using ClosedXML.Excel;
using CrombieConsole.model;
using CrombieConsole.Model;
using DocumentFormat.OpenXml.Drawing;
using Data.Repository.Intefaces;

namespace CrombieConsole.Data.Repository
{
    public class LibroExcelRepository : ILibroRepository
    {
        public string Filepath { get; set; }
        public LibroExcelRepository() {
            Filepath = "C:/Users/migue/source/repos/Crombie/CrombieConsole/BibliotecaBaseDatos.xlsx";
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                var worksheet = workbook.Worksheet(3);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                worksheet.Cell(lastRowUsed + 1, 1).Value = isbn;
                worksheet.Cell(lastRowUsed + 1, 2).Value = titulo;
                worksheet.Cell(lastRowUsed + 1, 3).Value = autor;
                worksheet.Cell(lastRowUsed + 1, 4).Value = "Disponible";

                workbook.Save();
            }
        }

        public void AgregarHistorialPrestamo(Libro libro, Usuario usuario)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                //Hoja de historial
                var worksheet = workbook.Worksheet(2);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                worksheet.Cell(lastRowUsed + 1, 1).Value = usuario.IdUsuario;
                worksheet.Cell(lastRowUsed + 1, 2).Value = usuario.Nombre;
                worksheet.Cell(lastRowUsed + 1, 3).Value = "Prestamo";
                worksheet.Cell(lastRowUsed + 1, 4).Value = libro.ISBN;
                worksheet.Cell(lastRowUsed + 1, 5).Value = libro.Titulo;

                workbook.Save();

                ActualizarDisponibilidadLibro(libro);
            }
        }

        public void AgregarHistorialDevolucion(Libro libro, Usuario usuario)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                //Hoja de historial
                var worksheet = workbook.Worksheet(2);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                worksheet.Cell(lastRowUsed + 1, 1).Value = usuario.IdUsuario;
                worksheet.Cell(lastRowUsed + 1, 2).Value = usuario.Nombre;
                worksheet.Cell(lastRowUsed + 1, 3).Value = "Devolucion";
                worksheet.Cell(lastRowUsed + 1, 4).Value = libro.ISBN;
                worksheet.Cell(lastRowUsed + 1, 5).Value = libro.Titulo;

                workbook.Save();

                ActualizarDisponibilidadLibro(libro);
            }
        }

        public List<HistorialBiblioteca> ObtenerHistorialBiblioteca()
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                var worksheet = workbook.Worksheet(2);
                var historialBiblioteca = new List<HistorialBiblioteca>();

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 2; row <= lastRowUsed; row++)
                {
                    var nuevoHistorial = new HistorialBiblioteca()
                    {
                        IdUsuario = worksheet.Cell(row, 1).GetValue<int>(),
                        ISBN = worksheet.Cell(row, 4).GetValue<int>()
                    };
                    if (worksheet.Cell(row, 3).GetValue<string>() == "Prestamo")
                    {
                        nuevoHistorial.Accion = Accion.Prestamo;
                    }
                    else nuevoHistorial.Accion = Accion.Devolucion;

                    historialBiblioteca.Add(nuevoHistorial);
                }
                return historialBiblioteca;
            }
        }

        public void ActualizarDisponibilidadLibro(Libro libro)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                var worksheet = workbook.Worksheet(3);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    var isbn = worksheet.Cell(row, 1).GetValue<int>();
                    if (isbn == libro.ISBN)
                    {
                        if (libro.Disponible == true)
                        {
                            worksheet.Cell(row, 4).Value = "No Disponible";

                        }
                        else worksheet.Cell(row, 4).Value = "Disponible";
                        break;
                    }
                }
                workbook.Save();
            }
        }

        public List<Libro> ObtenerLibros()
        {
            var dataList = new List<Libro>();

            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los libros es 3
                var worksheet = workbook.Worksheet(3);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    var disponible = worksheet.Cell(row, 4).GetValue<string>() == "Disponible" ? true : false;
                    var libro = new Libro
                    {
                        ISBN = worksheet.Cell(row, 1).GetValue<int>(),
                        Titulo = worksheet.Cell(row, 2).GetValue<string>(),
                        Autor = worksheet.Cell(row, 3).GetValue<string>(),
                        Disponible = disponible
                    };

                    dataList.Add(libro);
                }
            }
            return dataList;
        }

        public Libro ObtenerLibro(int isbn)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                // la hoja de excel que contiene los libros es 3
                var worksheet = workbook.Worksheet(3);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    var isbnExcel = worksheet.Cell(row, 1).GetValue<int>();
                    if (isbnExcel == isbn)
                    {
                        var disponible = worksheet.Cell(row, 4).GetValue<string>() == "Disponible" ? true : false;
                        var libro = new Libro
                        {
                            ISBN = isbnExcel,
                            Titulo = worksheet.Cell(row, 2).GetValue<string>(),
                            Autor = worksheet.Cell(row, 3).GetValue<string>(),
                            Disponible = disponible
                        };
                        return libro;
                    }
                }
            }
            throw new Exception("No se ha encontrado el libro");
        }

        public List<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario)
        {
            var historialBiblioteca = ObtenerHistorialBiblioteca();
            var librosBiblioteca = ObtenerLibros();
            List<Libro> librosPrestados = new List<Libro>();
            foreach (var historial in historialBiblioteca)
            {
                var libro = librosBiblioteca.FirstOrDefault(l => l.ISBN == historial.ISBN);
                if (historial.IdUsuario == idUsuario && historial.Accion == Accion.Prestamo && libro != null)
                {
                    librosPrestados.Add(libro);
                } else if(historial.IdUsuario == idUsuario && historial.Accion == Accion.Devolucion && libro != null)
                {
                    librosPrestados.Remove(libro);
                }
            }
            return librosPrestados;
        }

        public void ActualizarLibro(Libro libro)
        {
            using (var workbook = new XLWorkbook(Filepath))
            {
                var worksheet = workbook.Worksheet(3);

                int lastRowUsed = worksheet.LastRowUsed().RowNumber();

                for (int row = 3; row <= lastRowUsed; row++)
                {
                    var isbn = worksheet.Cell(row, 1).GetValue<int>();
                    if (isbn == libro.ISBN)
                    {
                        worksheet.Cell(row, 2).Value = libro.Titulo;
                        worksheet.Cell(row, 3).Value = libro.Autor;
                        break;
                    }
                }
                workbook.Save();
            }
        }
    }
}
