namespace Sucursales_CRUD_QUALA.Server.Models
{
    public class Respuesta<T>
    {
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
        public T? Dato { get; set; }
    }
}
