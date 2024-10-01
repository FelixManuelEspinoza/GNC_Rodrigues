using GNC_Rodrigues.BD.DATA.Entity;
using GNC_Rodrigues.BD.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace Proyecto2024.Server.Controllers
{
  namespace GNC_Rodrigues.server.Controllers
    {

        [ApiController]
        [Route("api/Vehiculos")]
        public class VehiculoController : ControllerBase
        {
            private readonly Context context;

            public VehiculoController(Context context)
            {
                this.context = context;

            }

            [HttpGet]
            public async Task<ActionResult<List<Vehiculo>>> get()
            {
                return await context.Vehiculos.ToListAsync();
            }



            [HttpPost]
            public async Task<ActionResult<string>> Post([FromBody] Vehiculo vehiculo)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                try
                {
                    // Verificar si ClienteDNI no es null y existe en la base de datos
                    if (!string.IsNullOrEmpty(vehiculo.ClienteDNI))
                    {
                        var clienteExistente = await context.Clientes.FindAsync(vehiculo.ClienteDNI);
                        if (clienteExistente == null)
                        {
                            return BadRequest("El ClienteDNI proporcionado no existe.");
                        }
                    }

                    // Verificar unicidad de Dominio
                    var existeDominio = await context.Vehiculos.AnyAsync(v => v.Dominio == vehiculo.Dominio);
                    if (existeDominio)
                    {
                        return BadRequest("El Dominio proporcionado ya existe.");
                    }

                    context.Vehiculos.Add(vehiculo);
                    await context.SaveChangesAsync();
                    return Ok(vehiculo.Dominio);
                }
                catch (Exception err)
                {
                    var errorMessage = err.InnerException != null ? err.InnerException.Message : err.Message;
                    return BadRequest($"Error al guardar los cambios: {errorMessage}");
                }
            }




            //[HttpPut("{dominio:int}")] //api/Vehiculos/2
            //public async Task<ActionResult> Put(int dominio,[FromBody] Vehiculo entidad)
            //{
            //    if (dominio != entidad.Dominio)
            //    {
            //        return BadRequest("Datos incorrectos");
            //    }

            //    var pepe = await context .Vehiculos.Where(e=> entidad.Dominio==dominio).FirstOrDefaultAsync();
            //}

            [HttpPut("{dominio}")] // api/Vehiculos/2
            public async Task<ActionResult> Put(string dominio, [FromBody] Vehiculo entidad)
            {
                
                if (dominio != entidad.Dominio)
                {
                    return BadRequest("Datos incorrectos, el dominio no coincide.");
                }

                
                var vehiculo = await context.Vehiculos.Where(e => e.Dominio == dominio).FirstOrDefaultAsync();

                
                if (vehiculo == null)
                {
                    return NotFound("Vehículo no encontrado.");
                }

                // Actualizar vehículo
                vehiculo.Marca = entidad.Marca;
                vehiculo.ClienteDNI = entidad.ClienteDNI;
                vehiculo.Dominio = entidad.Dominio;
                


                await context.SaveChangesAsync();

                return Ok("Vehículo actualizado correctamente.");
            }

            [HttpDelete("{dominio}")]
            public async Task<ActionResult> DeleteVehiculo(string dominio)
            {
                try
                {
                    // Buscar por patente
                    var vehiculo = await context.Vehiculos
                        .Where(v => v.Dominio == dominio)
                        .FirstOrDefaultAsync();

                    
                    if (vehiculo == null)
                    {
                        return NotFound("Vehículo no encontrado.");
                    }

                    // Eliminar
                    context.Vehiculos.Remove(vehiculo);
                    await context.SaveChangesAsync();

                    return Ok($"Vehículo con dominio {dominio} eliminado correctamente.");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

        }
    }
}
