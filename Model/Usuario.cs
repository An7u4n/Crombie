namespace CrombieConsole.model
{
    public class Usuario
    {
        public Usuario() { }
        public string Nombre { get; set; }
        public int id_usuario { get; set; }
        public static int LimiteLibros { get; set; }
        public virtual ICollection<Libro> LibrosPrestados { get; set; }
    }
}
