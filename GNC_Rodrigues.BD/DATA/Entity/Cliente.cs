using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GNC_Rodrigues.BD.DATA.Entity
{
    [Index(nameof(DNI), Name = "Cliente_UQ_DNI", IsUnique = true)]
    public class Cliente
    {
        [Key]
        [Required(ErrorMessage = "El número de documento es obligatoria.")]
        [MaxLength(12, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string DNI {  get; set; }

        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número de Telefono es obligatorio.")]
        [MaxLength(20, ErrorMessage = "Máximo número de caracteres {1}.")]
        public string Telefono { get; set; }

        public ICollection<Vehiculo>vehiculos { get; set; }

    }
}
