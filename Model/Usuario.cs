namespace CrombieConsole.model
{
    public abstract class Usuario
    {
        public string Nombre { get; set; }
        public int IdUsuario { get; set; }
        public int LimiteLibros { get; set; }
        public ICollection<Libro> LibrosPrestados { get; set; }

        public bool PrestarMaterial(Libro libro)
        {
            if(LibrosPrestados.Count < LimiteLibros)
            {
                LibrosPrestados.Add(libro);
                libro.Disponible = false;
                return true;
            } else
            {
                Console.WriteLine("Limite de libros alcanzado");
                Console.ReadLine();
            }
            return false;
        }
        public void DevolverMaterial(Libro libro)
        {
            LibrosPrestados.Remove(libro);
            libro.Disponible = true;
        }
    }
}
