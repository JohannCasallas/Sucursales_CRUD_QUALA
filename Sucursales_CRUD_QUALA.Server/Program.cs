using Microsoft.EntityFrameworkCore;
using Sucursales_CRUD_QUALA.Server.Data;
using System.Text.Json.Serialization;

// Crea un nuevo constructor de aplicaci�n web
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias

// Agrega el servicio de controladores MVC a la aplicaci�n
builder.Services.AddControllers();

// Configura y agrega el contexto de base de datos al contenedor de dependencias
builder.Services.AddDbContext<TestDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega servicios para la exploraci�n de API de extremos
builder.Services.AddEndpointsApiExplorer();

// Agrega generaci�n de documentaci�n Swagger
builder.Services.AddSwaggerGen();

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    });


// Construye la aplicaci�n
var app = builder.Build();

// Configura CORS para permitir solicitudes desde cualquier origen, m�todo y encabezado
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configura el uso de archivos est�ticos predeterminados
app.UseDefaultFiles();

// Configura el uso de archivos est�ticos
app.UseStaticFiles();

// Configura el pipeline de solicitud HTTP

// Si la aplicaci�n est� en modo de desarrollo, habilita Swagger y Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirecciona las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita la autorizaci�n
app.UseAuthorization();

// Mapea los controladores de API
app.MapControllers();

// Mapea la p�gina de inicio a un archivo HTML
app.MapFallbackToFile("/index.html");

// Ejecuta la aplicaci�n
app.Run();