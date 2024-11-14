namespace CrombieConsole.model
{
    public class Profesor : Usuario
    {
        public Profesor(string nombre, int id)
        {
            Nombre = nombre;
            IdUsuario = id;
            LibrosPrestados = new List<Libro>();
            LimiteLibros = 5;
        }
    }
}
