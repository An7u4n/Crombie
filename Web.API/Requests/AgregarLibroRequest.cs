namespace Web.API.Requests
{
    public class AgregarLibroRequest
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int ISBN { get; set; }
    }
}
