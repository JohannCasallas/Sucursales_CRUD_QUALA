using System;
using System.Collections.Generic;

namespace Sucursales_CRUD_QUALA.Server.Models
{
    public partial class SucursalJc
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int MonedaId { get; set; }

        public virtual MonedaJc? Moneda { get; set; } = null!;
    }
}
