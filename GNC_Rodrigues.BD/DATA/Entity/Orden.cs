using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GNC_Rodrigues.BD.DATA.Entity
{
    [Index(nameof(Id), Name = "Orden_UQ_Id", IsUnique = true)]

    public class Orden
    {
        public int Id { get; set; }

        [ForeignKey("Vehiculo")]
        public string VehiculoDominio { get; set; }
        public Vehiculo Vehiculo { get; set; }

        [ForeignKey("Cliente")]
        public string ClienteDNI { get; set; }
        public Cliente Cliente { get; set; }


        public string Detalles { get; set; }
        
        [Required(ErrorMessage = "El número de documento es obligatoria.")]
        [MaxLength(12, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Fallas { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Presupuesto { get; set; }


        public bool PresupuestoConfirmado { get; set; }

        
        public string Reparacion  { get; set; } 


        public bool AvisadoRetirar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "La Fecha es obligatoria.")]
        public DateTime FechaOrden { get; set; }


        public bool FuncionaNafta { get; set; }


        public bool CortaCorriente { get; set; }



    }
}
