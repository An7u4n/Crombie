namespace CrombieConsole.model
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public int Id { get; set; }
        public ICollection<Libro> LibrosPrestados { get; set; }

        public Usuario(string nombre, int id)
        {
            Nombre = nombre;
            Id = id;
            LibrosPrestados = new List<Libro>();
        }
    }
}
