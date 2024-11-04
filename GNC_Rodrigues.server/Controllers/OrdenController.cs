using GNC_Rodrigues.BD.DATA.Entity;
using GNC_Rodrigues.BD.DATA;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GNC_Rodrigues.server.Controllers
{
    [ApiController]
    [Route("api/Ordenes")]
    public class OrdenController : ControllerBase
    {
        private readonly Context context;

        public OrdenController(Context context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Orden>>> Get()
        {
            return await context.Ordenes.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Orden>> GetById(int id)
        {
            var orden = await context.Ordenes.Include(o => o.Vehiculo)
                                             .Include(o => o.Cliente)
                                             .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                return NotFound("Orden no encontrada.");
            }

            return orden;
        }

        // nuevo método de búsqueda
        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Orden>>> Buscar(string query)
        {
            var terminos = query.Split('-');
            var criterio = terminos[0];
            var valor = terminos[1];

            IQueryable<Orden> consulta = context.Ordenes.Include(o => o.Cliente).Include(o => o.Vehiculo);

            if (criterio == "vehiculo")
            {
                consulta = consulta.Where(o => o.Vehiculo.Marca.Contains(valor));
            }
            else if (criterio == "dominio")
            {
                consulta = consulta.Where(o => o.Vehiculo.Dominio.Contains(valor));
            }

            var resultados = await consulta.ToListAsync();

            return Ok(resultados);
        }


        [HttpPost]
        public async Task<ActionResult<int>> Post(Orden entidad)
        {
            context.Ordenes.Add(entidad);
            await context.SaveChangesAsync();
            return Ok(entidad.Id);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Orden entidad)
        {
            if (id != entidad.Id)
            {
                return BadRequest("Datos incorrectos.");
            }

            var ordenExistente = await context.Ordenes.FirstOrDefaultAsync(o => o.Id == id);

            if (ordenExistente == null)
            {
                return NotFound("Orden no encontrada.");
            }

            // Actualizar los campos que necesitas
            ordenExistente.FechaOrden = entidad.FechaOrden;
            ordenExistente.FuncionaNafta = entidad.FuncionaNafta;
            ordenExistente.CortaCorriente = entidad.CortaCorriente;
            ordenExistente.VehiculoDominio = entidad.VehiculoDominio;
            ordenExistente.ClienteDNI = entidad.ClienteDNI;
            ordenExistente.CortaCorriente = entidad.CortaCorriente;
            ordenExistente.Presupuesto = entidad.Presupuesto;
            ordenExistente.Detalles = entidad.Detalles;
            ordenExistente.Reparacion = entidad.Reparacion;
            ordenExistente.AvisadoRetirar = entidad.AvisadoRetirar;
            ordenExistente.PresupuestoConfirmado = entidad.PresupuestoConfirmado;

            await context.SaveChangesAsync();
            return Ok("Orden actualizada correctamente.");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orden = await context.Ordenes.FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null)
            {
                return NotFound("Orden no encontrada.");
            }

            context.Ordenes.Remove(orden);
            await context.SaveChangesAsync();

            return Ok($"Orden con ID {id} eliminada correctamente.");
        }
    }
 }

