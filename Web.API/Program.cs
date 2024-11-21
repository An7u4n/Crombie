using CrombieConsole.Infrastructure.Repository;
using CrombieConsole.Services;
using Infrastructure.Repository.Intefaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BibliotecaService>();
builder.Services.AddSingleton<ProfesorService>();
builder.Services.AddSingleton<EstudianteService>();
builder.Services.AddSingleton<ILibroRepository, LibroExcelRepository>();
builder.Services.AddSingleton<IUsuarioRepository, UsuarioExcelRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
