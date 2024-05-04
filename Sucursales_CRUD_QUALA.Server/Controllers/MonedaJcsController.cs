using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sucursales_CRUD_QUALA.Server.Data;
using Sucursales_CRUD_QUALA.Server.Models;

namespace Sucursales_CRUD_QUALA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedaJcsController : ControllerBase
    {
        private readonly TestDBContext _context;

        public MonedaJcsController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/MonedaJcs
        [HttpGet("ObtenerMonedas")]
        public async Task<ActionResult<Respuesta<IEnumerable<MonedaJc>>>> ObtenerMonedas()
        {
            try
            {
                var monedas = await _context.MonedaJcs.ToListAsync();
                return Ok(new Respuesta<IEnumerable<MonedaJc>>
                {
                    Exito = true,
                    Mensaje = "Monedas obtenidas exitosamente.",
                    Dato = monedas
                });
            }
            catch (Exception ex)
            {
                return Problem("Error al obtener las monedas: " + ex.Message);
            }
        }

        private bool MonedaJcExists(int id)
        {
            return _context.MonedaJcs.Any(e => e.MonedaId == id);
        }
    }
}
