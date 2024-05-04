using System;
using System.Collections.Generic;

namespace Sucursales_CRUD_QUALA.Server.Models
{
    public partial class MonedaJc
    {
        public MonedaJc()
        {
            SucursalJcs = new HashSet<SucursalJc>();
        }

        public int MonedaId { get; set; }
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<SucursalJc> SucursalJcs { get; set; }
    }
}
