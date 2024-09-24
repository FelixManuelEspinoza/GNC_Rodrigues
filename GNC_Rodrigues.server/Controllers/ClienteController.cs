using GNC_Rodrigues.BD.DATA;
using GNC_Rodrigues.BD.DATA.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GNC_Rodrigues.server.Controllers
{
    [ApiController]
    [Route("api/Clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly Context context;

        public ClienteController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await context.Clientes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post(Cliente entidad)
        {
            try
            {
                context.Clientes.Add(entidad);
                await context.SaveChangesAsync();
                return Ok(entidad.DNI);
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{dni}")]
        public async Task<ActionResult> Put(string dni, [FromBody] Cliente entidad)
        {
            try
            {
             
                if (dni != entidad.DNI)
                {
                    return BadRequest("Datos incorrectos, el DNI no coincide.");
                }

            
                var cliente = await context.Clientes
                    .Where(c => c.DNI == dni)
                    .FirstOrDefaultAsync();

              
                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado.");
                }

                // Actualizar cliente
                cliente.Nombre = entidad.Nombre;
                cliente.Telefono = entidad.Telefono;
                cliente.DNI = entidad.DNI;


                await context.SaveChangesAsync();

                return Ok("Cliente actualizado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{dni}")]
        public async Task<ActionResult> DeleteCliente(string dni)
        {
            try
            {
                // Buscar  DNI
                var cliente = await context.Clientes
                    .Where(c => c.DNI == dni)
                    .FirstOrDefaultAsync();

               
                if (cliente == null)
                {
                    return NotFound("Cliente no encontrado.");
                }

                // Eliminar
                context.Clientes.Remove(cliente);
                await context.SaveChangesAsync();

                return Ok($"Cliente con DNI {dni} eliminado correctamente.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }

}