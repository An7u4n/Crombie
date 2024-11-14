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
    }
}
