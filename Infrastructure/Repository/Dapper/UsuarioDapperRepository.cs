using CrombieConsole.model;
using Dapper;
using Data.Repository.Intefaces;
using System.Transactions;

namespace Data.Repository.Dapper;
public class UsuarioDapperRepository : IUsuarioRepository
{
    private readonly DapperContext _context;

    public UsuarioDapperRepository(DapperContext context)
    {
        _context = context;
    }

    public ICollection<Estudiante> ObtenerEstudiantes()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                var sql = "select u.id_usuario, u.nombre from Estudiante e join Usuario u on e.id_usuario = u.id_usuario;";
                return connection.Query<Estudiante>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
            }
        }
    }

    public ICollection<Profesor> ObtenerProfesores()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                var sql = "select u.id_usuario, u.nombre from Profesor e join Usuario u on e.id_usuario = u.id_usuario;";
                return connection.Query<Profesor>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
            }
        }
    }

    public Usuario ObtenerUsuario(int idUsuario)
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                var sql = "SELECT * FROM Usuario WHERE id_usuario = @idUsuario";
                return connection.QueryFirstOrDefault<Usuario>(sql, new { idUsuario });
            }
            catch (Exception ex)
            {
                throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
            }
        }
    }

    public ICollection<Usuario> ObtenerUsuarios()
    {
        using (var connection = _context.CreateConnection())
        {
            try
            {
                var sql = "SELECT * FROM Usuario";
                return connection.Query<Usuario>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error intentando ejecutar consulta: " + ex.Message);
            }
        }
    }

    public void RegistrarEstudiante(Estudiante estudiante)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertUsuarioQuery = @"
                    INSERT INTO Usuario (Nombre)
                    VALUES (@Nombre);
                    SELECT SCOPE_IDENTITY();";

                    var usuarioId = connection.QuerySingle<int>(insertUsuarioQuery,
                    new { Nombre = estudiante.Nombre},
                    transaction);

                    string insertEstudianteQuery = @"
                    INSERT INTO Estudiante (id_usuario)
                    VALUES (@Id_usuario);";

                    connection.Execute(insertEstudianteQuery,
                    new { Id_usuario = usuarioId },
                    transaction);

                    transaction.Commit();

                    Console.WriteLine("Inserción completada exitosamente.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error intentando registrar estudiante: " + ex.Message);
                }
            }
        }
    }

    public void RegistrarProfesor(Profesor profesor)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string insertUsuarioQuery = @"
                    INSERT INTO Usuario (Nombre)
                    VALUES (@Nombre);
                    SELECT SCOPE_IDENTITY();";

                    var usuarioId = connection.QuerySingle<int>(insertUsuarioQuery,
                    new { Nombre = profesor.Nombre },
                    transaction);

                    string insertProfesorQuery = @"
                    INSERT INTO Profesor (id_usuario)
                    VALUES (@Id_usuario);";

                    connection.Execute(insertProfesorQuery,
                    new { Id_usuario = usuarioId },
                    transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error intentando registrar profesor: " + ex.Message);
                }
            }
        }
    }
}
