using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    public class DetalleVentaId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")] // minimo 5 digitos
        [Display(Name = "Detalle Venta ID")]
        public int id_detalle { get; set; }

    }
}
