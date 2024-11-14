using CrombieConsole.model;
using CrombieConsole.Services;

var biblioteca = new Biblioteca();
var excelService = new ExcelService(AppDomain.CurrentDomain.BaseDirectory + "../../../BibliotecaBaseDatos.xlsx");
var bibliotecaService = new BibliotecaService(biblioteca, excelService);

bibliotecaService.CargarDatos();

while (true)
{
    Console.Clear();
    mostrarMenu();
}

void AgregarUsuario()
{
    Console.WriteLine("Ingrese el nombre de usuario");
    var nombre = Console.ReadLine();
    Console.WriteLine("Ingrese el numero de id de usuario");
    int id;
    var idParse = int.TryParse(Console.ReadLine(), out id);
    if (idParse == false || nombre == null)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }
    int tipoUsuario;
    Console.WriteLine("Ingrese el tipo de usuario (1. Profesor, 2. Estudiante)");
    var tipoUsuarioParse = int.TryParse(Console.ReadLine(), out tipoUsuario);

    if (tipoUsuarioParse == false)
    {
        Console.WriteLine("Opcion Incorrecta");
        return;
    }

    if(tipoUsuario == 1) 
        bibliotecaService.RegistrarProfesor(nombre, id);
    else if (tipoUsuario == 2) 
        bibliotecaService.RegistrarEstudiante(nombre, id);
    else Console.WriteLine("Opcion Incorrecta");
    Console.ReadLine();
}

void DevolverLibro()
{
    Console.WriteLine("Ingrese el id del usuario");
    var usuario = int.Parse(Console.ReadLine());
    Console.WriteLine("Ingrese el isbn del libro");
    var isbn = int.Parse(Console.ReadLine());
    bibliotecaService.DevolverLibro(isbn, usuario);
    Console.ReadLine();
}

void VerEstadoDeLosLibros()
{
    bibliotecaService.VerEstadoLibros();
    Console.ReadLine();
}

void VerLibrosPrestadosUsuario()
{
    Console.WriteLine("Ingrese el id del usuario");
    var usuario = int.Parse(Console.ReadLine());

    bibliotecaService.MostrarLibrosPrestadosUsuario(usuario);
    Console.ReadLine();

}

void AgregarLibro()
{
    Console.WriteLine("Ingrese el titulo");
    var titulo = Console.ReadLine();
    Console.WriteLine("Ingrese el autor");
    var autor = Console.ReadLine();
    Console.WriteLine("Ingrese el numero de isbn");
    int isbn;
    var isbnParse = int.TryParse(Console.ReadLine(), out isbn);
    if (titulo == null || autor == null || isbnParse == false)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }
    bibliotecaService.AgregarLibro(titulo, autor, isbn);
}

void PrestarLibro()
{
    Console.WriteLine("Ingrese el id del usuario");
    int idUsuario;
    var usuarioParse = int.TryParse(Console.ReadLine(), out idUsuario);
    Console.WriteLine("Ingrese el isbn del libro a prestar");
    int isbn;
    var isbnParse = int.TryParse(Console.ReadLine(), out isbn);
    if (usuarioParse == false || isbnParse == false)
    {
        Console.WriteLine("Algun campo mal ingresado");
        return;
    }

    bibliotecaService.PrestarLibro(idUsuario, isbn);
    Console.ReadLine();
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
        case 7:
            Environment.Exit(0);
            break;
        case 8:
            foreach(var u in biblioteca.Usuarios) Console.WriteLine(u.Nombre);
            Console.ReadLine();
            break;
        default:
            Console.WriteLine("Opcion Invalida"); break;
    }
}
