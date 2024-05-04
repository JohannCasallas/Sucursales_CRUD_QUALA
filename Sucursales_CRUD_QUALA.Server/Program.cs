using Microsoft.EntityFrameworkCore;
using Sucursales_CRUD_QUALA.Server.Data;
using System.Text.Json.Serialization;

// Crea un nuevo constructor de aplicación web
var builder = WebApplication.CreateBuilder(args);

// Agrega servicios al contenedor de dependencias

// Agrega el servicio de controladores MVC a la aplicación
builder.Services.AddControllers();

// Configura y agrega el contexto de base de datos al contenedor de dependencias
builder.Services.AddDbContext<TestDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agrega servicios para la exploración de API de extremos
builder.Services.AddEndpointsApiExplorer();

// Agrega generación de documentación Swagger
builder.Services.AddSwaggerGen();

//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//    });


// Construye la aplicación
var app = builder.Build();

// Configura CORS para permitir solicitudes desde cualquier origen, método y encabezado
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

// Configura el uso de archivos estáticos predeterminados
app.UseDefaultFiles();

// Configura el uso de archivos estáticos
app.UseStaticFiles();

// Configura el pipeline de solicitud HTTP

// Si la aplicación está en modo de desarrollo, habilita Swagger y Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirecciona las solicitudes HTTP a HTTPS
app.UseHttpsRedirection();

// Habilita la autorización
app.UseAuthorization();

// Mapea los controladores de API
app.MapControllers();

// Mapea la página de inicio a un archivo HTML
app.MapFallbackToFile("/index.html");

// Ejecuta la aplicación
app.Run();