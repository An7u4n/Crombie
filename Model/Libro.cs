namespace CrombieConsole.model
{
    public class Libro
    {
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public int ISBN { get; set; }
        public bool Disponible { get; set; }
        public Libro() { }
        public Libro(string titulo, string autor, int isbn)
        {
            Autor = autor;
            Titulo = titulo;
            ISBN = isbn;
            Disponible = true;
        }
    }
}
