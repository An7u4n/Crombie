using CrombieConsole.model;


namespace CrombieConsole.Services
{
    public class BibliotecaService
    {
        private Biblioteca Biblioteca = new Biblioteca();
        public ExcelService ExcelService = new ExcelService("C:/Users/migue/source/repos/Crombie/CrombieConsole/BibliotecaBaseDatos.xlsx");
        public BibliotecaService()
        {
            ExcelService.CargarDatosExcel(Biblioteca);

        }

        public void DevolverLibro(int isbn, int idUsuario)
        {
            var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuario Inexistente");
            }

            var libro = usuario.LibrosPrestados.FirstOrDefault(l => l.ISBN == isbn);
            if (libro == null)
            {
                throw new Exception("Libro no prestado");
            }
            libro.Disponible = true;
            usuario.LibrosPrestados.Remove(libro);
            ExcelService.AgregarHistorialDevolucion(libro, usuario);
        }

        public void PrestarLibro(int idUsuario, int isbn)
        {

            var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);

            var libroAPrestar = Biblioteca.Libros.FirstOrDefault(l => l.ISBN == isbn);
            if (libroAPrestar != null && libroAPrestar.Disponible == true && usuario != null)
            {
                if(usuario.PrestarMaterial(libroAPrestar) == true)
                {
                    Console.WriteLine("Libro prestado");
                } else
                {
                    throw new Exception("No se presta el libro, limite alcanzado");
                }
                ExcelService.AgregarHistorialPrestamo(libroAPrestar, usuario);
            }else throw new Exception("Libro o usuario no existentes");
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            try
            {
                Biblioteca.Libros.Add(new Libro(titulo, autor, isbn));
                ExcelService.AgregarLibro(titulo, autor, isbn);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RegistrarEstudiante(string nombre, int id)
        {
            if (Biblioteca.Usuarios.Any(u => u.IdUsuario == id) == false)
                Biblioteca.Usuarios.Add(new Estudiante(nombre, id));
            else
            {
                Console.WriteLine("Id de usuario ya usada");
            }
        }

        public void RegistrarProfesor(string nombre, int id)
        {
            if (Biblioteca.Usuarios.Any(u => u.IdUsuario == id) == false)
                Biblioteca.Usuarios.Add(new Profesor(nombre, id));
            else
            {
                Console.WriteLine("Id de usuario ya usada");
            }
        }

        public List<Libro> ObtenerLibros()
        {
            return Biblioteca.Libros.ToList();
        }

        public List<Libro> LibrosPrestadosUsuario(int idUsuario)
        {
            var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                throw new Exception("Usuario Inexistente");
            }
            return usuario.LibrosPrestados.ToList();
        }
    }
}
