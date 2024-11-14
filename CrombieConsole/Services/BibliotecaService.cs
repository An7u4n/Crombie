using CrombieConsole.model;
using DocumentFormat.OpenXml.Bibliography;

namespace CrombieConsole.Services
{
    public class BibliotecaService
    {
        private Biblioteca Biblioteca;
        public ExcelService ExcelService { get; set; }
        public BibliotecaService(Biblioteca biblioteca, ExcelService excelService)
        {
            Biblioteca = biblioteca;
            ExcelService = excelService;
        }

        public void CargarDatos()
        {
            ExcelService.CargarDatosExcel(Biblioteca);
        }

        public void DevolverLibro(int isbn, int idUsuario)
        {
            var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                Console.WriteLine("Usuario Inexistente");
                return;
            }

            var libro = usuario.LibrosPrestados.FirstOrDefault(l => l.ISBN == isbn);
            if (libro == null)
            {
                Console.WriteLine("Libro no prestado");
                return;
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
                    Console.WriteLine("No se presto el libro, limite alcanzado");
                }
                ExcelService.AgregarHistorialPrestamo(libroAPrestar, usuario);
            }
        }

        public void MostrarLibrosDisponibles()
        {
            foreach (var libro in Biblioteca.Libros)
            {
                if (libro.Disponible == true)
                {
                    Console.WriteLine($"Titulo: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}");
                }
            }
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            Biblioteca.Libros.Add(new Libro(titulo, autor, isbn));
            ExcelService.AgregarLibro(titulo, autor, isbn);
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
        public void VerEstadoLibros()
        {
            foreach (var libro in Biblioteca.Libros)
            {
                Console.Write($"Titulo: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}, Disponible: ");
                if (libro.Disponible == true) Console.WriteLine("Si");
                else Console.WriteLine("No");
            }
        }
        public void MostrarLibrosPrestadosUsuario(int idUsuario)
        {
            var usuario = Biblioteca.Usuarios.FirstOrDefault(u => u.IdUsuario == idUsuario);
            if (usuario == null)
            {
                Console.WriteLine("Usuario Inexistente");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Lista de libros prestadas a " + usuario.Nombre);
            foreach (var libro in usuario.LibrosPrestados)
            {
                Console.Write($"Titulo: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}");
            }
        }
    }
}
