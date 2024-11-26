namespace Services.Interfaces
{
    public interface IBibliotecaService
    {
        void AgregarLibro(string titulo, string autor, int isbn);
        void DevolverLibro(int isbn, int idUsuario);
        void PrestarLibro(int idUsuario, int isbn);
        void ActualizarLibro(int isbn, string titulo, string autor);
    }
}