using CrombieConsole.Data.Repository;
using CrombieConsole.Services;
using Data.Repository;
using Data.Repository.Intefaces;
using Services;
using Services.Interfaces;
using Web.API.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBibliotecaService, BibliotecaService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<ILibroService, LibroService>();
builder.Services.AddScoped<ILibroRepository, LibroExcelRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioExcelRepository>();

var logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "api-errors.log");

Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

builder.Services.AddSingleton(new FileLogger(logFilePath));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorLoggingMiddleware>();

app.MapControllers();

app.Run();
