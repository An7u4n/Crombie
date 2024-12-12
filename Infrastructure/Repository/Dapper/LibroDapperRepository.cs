using CrombieConsole.model;
using CrombieConsole.Model;
using Dapper;
using Data.Repository.Intefaces;

namespace Data.Repository.Dapper
{
    public class LibroDapperRepository : ILibroRepository
    {
        private readonly DapperContext _context;

        public LibroDapperRepository(DapperContext context)
        {
            _context = context;
        }

        public ICollection<Libro> ObtenerLibros()
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM Libro";
                    return connection.Query<Libro>(sql).ToList();
                }
                catch(Exception ex)
                {
                    throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
                }
            }
        }

        public Libro ObtenerLibro(int isbn) 
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM Libro WHERE ISBN = @isbn";
                    return connection.QueryFirstOrDefault<Libro>(sql, new { isbn });
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
                }
            }
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "INSERT INTO Libro (Titulo, Autor, ISBN) VALUES (@titulo, @autor, @isbn)";
                    connection.Execute(sql, new { titulo, autor, isbn });
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando insertar entidad: " + ex.Message);
                }
            }
        }

        public ICollection<HistorialBiblioteca> ObtenerHistorialBiblioteca()
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM HistorialBiblioteca";
                    return connection.Query<HistorialBiblioteca>(sql).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
                }
            }
        }

        public void AgregarHistorialPrestamo(Libro libro, Usuario usuario)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "INSERT INTO HistorialBiblioteca (id_usuario, isbn, fecha_prestamo, accion) VALUES (@IdUsuario, @ISBN, @FechaPrestamo, @Accion)";
                    connection.Execute(sql, new { IdUsuario = usuario.id_usuario, ISBN = libro.ISBN, FechaPrestamo = DateTime.Now, Accion = Accion.Prestamo });
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando insertar entidad: " + ex.Message);
                }
            }
        }

        public void AgregarHistorialDevolucion(Libro libro, Usuario usuario)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "INSERT INTO HistorialBiblioteca (id_usuario, isbn, fecha_prestamo, accion) VALUES (@IdUsuario, @ISBN, @FechaPrestamo, @Accion)";
                    connection.Execute(sql, new { IdUsuario = usuario.id_usuario, ISBN = libro.ISBN, FechaPrestamo = DateTime.Now, Accion = Accion.Devolucion });
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando insertar entidad: " + ex.Message);
                }
            }
        }

        public void ActualizarDisponibilidadLibro(Libro libro)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "UPDATE Libro SET Disponible = @Disponible WHERE ISBN = @ISBN";
                    connection.Execute(sql, libro);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentado ejecutar sql" + ex.Message);
                }
            }
        }

        public ICollection<Libro> ObtenerLibrosPrestadosAUsuario(int idUsuario)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "SELECT * FROM Libro WHERE ISBN IN (SELECT ISBN FROM HistorialBiblioteca WHERE id_usuario = @idUsuario)";
                    return connection.Query<Libro>(sql, new { idUsuario }).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentado ejecutar sql" + ex.Message);
                }
            }
        }

        public void ActualizarLibro(Libro libro)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "UPDATE Libro SET Titulo = @Titulo, Autor = @Autor WHERE ISBN = @ISBN";
                    connection.Execute(sql, libro);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando actualizar entidad: " + ex.Message);
                }
            }
        }

        public void EliminarLibro(Libro libro)
        {
            using (var connection = _context.CreateConnection())
            {
                try
                {
                    var sql = "DELETE FROM Libro WHERE ISBN = @ISBN";
                    connection.Execute(sql, libro);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error intentando eliminar entidad: " + ex.Message);
                }
            }
        }
    }
}