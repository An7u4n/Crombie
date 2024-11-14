namespace CrombieConsole.model
{
    public class Estudiante : Usuario
    {
        public Estudiante(string nombre, int id) 
        {
            Nombre = nombre;
            IdUsuario = id;
            LibrosPrestados = new List<Libro>();
            LimiteLibros = 3;
        }
    }
}
