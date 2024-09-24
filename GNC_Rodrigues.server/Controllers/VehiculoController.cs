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

            public async Task<ActionResult<int>> post(Vehiculo entidad)
            {
                try
                {
                    context.Vehiculos.Add(entidad);
                    await context.SaveChangesAsync();
                    return Ok(entidad.Dominio);

                }
                catch (Exception err)
                {
                    return BadRequest(err.Message);
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
