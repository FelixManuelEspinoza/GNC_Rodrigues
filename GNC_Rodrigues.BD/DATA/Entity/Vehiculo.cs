using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNC_Rodrigues.BD.DATA.Entity
{
    [Index(nameof(Dominio), Name = "Vehiculo_UQ_Dominio", IsUnique = true)]
    public class Vehiculo
    {
        [Key]
        [Required(ErrorMessage = "El número de Patemte es obligatorio.")]
        [MaxLength(12, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Dominio { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [MaxLength(12, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Marca { get; set; }

        [ForeignKey("Cliente")]
        public string ClienteDNI { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<Orden> Ordenes { get; set; }
    }
}
