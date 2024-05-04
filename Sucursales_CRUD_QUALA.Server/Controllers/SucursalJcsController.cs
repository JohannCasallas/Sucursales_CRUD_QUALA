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
    public class SucursalJcsController : ControllerBase
    {
        private readonly TestDBContext _context;

        public SucursalJcsController(TestDBContext context)
        {
            _context = context;
        }

        // GET: api/SucursalJcs/ObtenerSucursal
        [HttpGet("ObtenerSucursales")]
        public async Task<ActionResult<Respuesta<IEnumerable<SucursalJc>>>> ObtenerSucursales()
        {
            try
            {
                var sucursales = await _context.SucursalJcs.ToListAsync();
                return Ok(new Respuesta<IEnumerable<SucursalJc>>
                {
                    Exito = true,
                    Mensaje = "Sucursales obtenidas exitosamente.",
                    Dato = sucursales
                });
            }
            catch (Exception ex)
            {
                return Problem("Error al obtener las sucursales: " + ex.Message);
            }
        }

        // DELETE: api/SucursalJcs/EliminarSucursal/{id}
        [HttpDelete("EliminarSucursal/{id}")]
        public async Task<ActionResult<Respuesta<string>>> EliminarSucursal(int id)
        {
            try
            {
                var sucursal = await _context.SucursalJcs.FindAsync(id);
                if (sucursal == null)
                {
                    return NotFound();
                }

                _context.SucursalJcs.Remove(sucursal);
                await _context.SaveChangesAsync();

                return Ok(new Respuesta<string>
                {
                    Exito = true,
                    Mensaje = "Sucursal eliminada exitosamente.",
                    Dato = "Success"
                });
            }
            catch (Exception ex)
            {
                return Problem("Error al eliminar la sucursal: " + ex.Message);
            }
        }

        // PUT: api/SucursalJcs/ActualizarSucursal/{id}
        [HttpPut("ActualizarSucursal/{id}")]
        public async Task<IActionResult> ActualizarSucursal(int id, [FromBody] SucursalJc sucursal)
        {
            if (id != sucursal.Codigo)
            {
                return BadRequest(new Respuesta<object>
                {
                    Exito = false,
                    Mensaje = "El ID proporcionado no coincide con el ID de la sucursal.",
                    Dato = null
                });
            }

            _context.Entry(sucursal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SucursalJcExists(id))
                {
                    return NotFound(new Respuesta<object>
                    {
                        Exito = false,
                        Mensaje = "La sucursal no se encontró.",
                        Dato = null
                    });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SucursalJcs/CrearSucursal
        [HttpPost("CrearSucursal")]
        public async Task<ActionResult<Respuesta<SucursalJc>>> CrearSucursal([FromBody] SucursalJc sucursal)
        {
            try
            {
                _context.SucursalJcs.Add(sucursal);
                await _context.SaveChangesAsync();

                return Ok(new Respuesta<SucursalJc>
                {
                    Exito = true,
                    Mensaje = "Sucursal creada exitosamente.",
                    Dato = sucursal
                });
            }
            catch (Exception ex)
            {
                return Problem("Error al crear la sucursal: " + ex.Message);
            }
        }

        private bool SucursalJcExists(int id)
        {
            return _context.SucursalJcs.Any(e => e.Codigo == id);
        }
    }
}
