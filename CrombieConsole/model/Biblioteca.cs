namespace CrombieConsole.model
{
    public class Biblioteca
    {
        public ICollection<Libro> Libros { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public Biblioteca()
        {
            Libros = new List<Libro>();
            Usuarios = new List<Usuario>();
        }

        public void AgregarLibro(string titulo, string autor, int isbn)
        {
            Libros.Add(new Libro(titulo, autor, isbn));
        }

        public void RegistrarUsuario(string nombre, int id)
        {
            if (Usuarios.Any(u => u.Id == id) == false)
                Usuarios.Add(new Usuario(nombre, id));
            else { 
                Console.WriteLine("Id de usuario ya usada"); 
                Console.ReadLine();
            }
        }

        public void PrestarLibro(int idUsuario, int isbn)
        {
            var libro = Libros.FirstOrDefault(l => l.ISBN == isbn);
            var usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            if (libro != null && libro.Disponible == true && usuario != null)
            {
                libro.Disponible = false;
                usuario.LibrosPrestados.Add(libro);
            }
            else
            {
                Console.WriteLine("Libro o usuario inexistentes o libro no prestado");
                Console.ReadLine();
            }
        }

        public void DevolverLibro(int isbn, int idUsuario)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            if(usuario == null)
            {
                Console.WriteLine("Usuario Inexistente");
                Console.ReadLine();
                return;
            }

            var libro = usuario.LibrosPrestados.FirstOrDefault(l => l.ISBN == isbn);
            if(libro == null)
            {
                Console.WriteLine("Libro no prestado");
                Console.ReadLine();
                return;
            }
            libro.Disponible = true;
            usuario.LibrosPrestados.Remove(libro);
        }

        public void VerEstadoLibros()
        {
            foreach(var libro in this.Libros)
            {
                Console.Write($"Titulo: {libro.Titulo}, Autor: {libro.Autor}, ISBN: {libro.ISBN}, Disponible: ");
                if (libro.Disponible == true) Console.WriteLine("Si");
                else Console.WriteLine("No");

                Console.ReadLine();
            }
        }

        public void MostrarLibrosPrestadosUsuario(int idUsuario)
        {
            var usuario = Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            if(usuario == null)
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
            Console.ReadLine();
        }
    }
}
