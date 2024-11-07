using CrombieConsole.model;
using System.Runtime.CompilerServices;

var biblioteca = new Biblioteca();

while (true)
{
    Console.Clear();
    mostrarMenu();
}

void AgregarLibro() {
    Console.WriteLine("Ingrese el titulo");
    var titulo = Console.ReadLine();
    Console.WriteLine("Ingrese el autor");
    var autor = Console.ReadLine();
    Console.WriteLine("Ingrese el numero de isbn");
    int isbn;
    var isbnParse = int.TryParse(Console.ReadLine(), out isbn);
    if(titulo == null || autor == null || isbnParse == false)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }
    biblioteca.AgregarLibro(titulo, autor, isbn);
}

void AgregarUsuario()
{
    Console.WriteLine("Ingrese el nombre de usuario");
    var nombre = Console.ReadLine();
    Console.WriteLine("Ingrese el numero de id de usuario");
    int id;
    var idParse = int.TryParse(Console.ReadLine(), out id);
    if(idParse == false || nombre == null)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }
    biblioteca.RegistrarUsuario(nombre, id);
}

void PrestarLibro()
{
    Console.WriteLine("Ingrese el id del usuario");
    int idUsuario;
    var usuarioParse = int.TryParse(Console.ReadLine(), out idUsuario);
    Console.WriteLine("Ingrese el isbn del libro");
    int isbn;
    var isbnParse = int.TryParse(Console.ReadLine(), out isbn);
    if(usuarioParse == false || isbnParse == false)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }
    biblioteca.PrestarLibro(idUsuario, isbn);
}

void DevolverLibro()
{
    Console.WriteLine("Ingrese el id del usuario");
    var usuario = int.Parse(Console.ReadLine());
    Console.WriteLine("Ingrese el isbn del libro");
    var isbn = int.Parse(Console.ReadLine());

    biblioteca.DevolverLibro(isbn, usuario);
}

void VerEstadoDeLosLibros()
{
    biblioteca.VerEstadoLibros();
}

void VerLibrosPrestadosUsuario()
{
    Console.WriteLine("Ingrese el id del usuario");
    var usuario = int.Parse(Console.ReadLine());

    biblioteca.MostrarLibrosPrestadosUsuario(usuario);
}

void mostrarMenu()
{
    Console.WriteLine("1. Agregar libro");
    Console.WriteLine("2. Agregar usuario");
    Console.WriteLine("3. Prestar libro");
    Console.WriteLine("4. Devolver libro");
    Console.WriteLine("5. Ver Estado de los Libros");
    Console.WriteLine("6. Ver Libros Prestados de un Usuario");
    Console.WriteLine("7. Salir");
    Console.WriteLine("Seleccione una Opcion:");

    int opcion;
    var opcionValida = int.TryParse(Console.ReadLine(), out opcion);
    
    if(opcionValida == false)
    {
        Console.WriteLine("Opcion Invalida");
        return;
    }

    switch (opcion)
    {
        case 1:
            AgregarLibro();
            break;
        case 2:
            AgregarUsuario();
            break;
        case 3:
            PrestarLibro();
            break;
        case 4:
            DevolverLibro();
            break;
        case 5:
            VerEstadoDeLosLibros();
            break;
        case 6:
            VerLibrosPrestadosUsuario();
            break;
        default:
            Console.WriteLine("Opcion Invalida"); break;
    }
}
