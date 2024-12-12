using CrombieConsole.model;

namespace CrombieConsole.Model
{
    public class HistorialBiblioteca
    {
        public int IdUsuario { get; set; }
        public int ISBN { get; set; }
        public Accion Accion { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public virtual Libro Libro { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
