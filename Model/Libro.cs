using CrombieConsole.Model;

namespace CrombieConsole.model
{
    public class Libro
    {
        public Libro() { }
        public Libro(string titulo, string autor, int isbn)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = true;
        }

        public string Autor { get; set; }
        public string Titulo { get; set; }
        public int ISBN { get; set; }
        public bool Disponible { get; set; }
        public virtual ICollection<HistorialBiblioteca> HistorialPrestamos { get; set; }
    }
}
