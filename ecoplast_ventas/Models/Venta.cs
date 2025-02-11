using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Venta")]

    public class Venta
    {
        [Key]
        [Required(ErrorMessage = "Escriba el codigo Venta")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")] // minimo 5 digitos
        [Display(Name = "Venta codigo")]
        public String id_venta { get; set; }

        [Required(ErrorMessage = "Elija fecha")]
        [Display(Name = "Fecha venta")]
        [DataType(DataType.Date)]
        public DateTime fecha { get; set; }

        [Required(ErrorMessage = "Escriba el ID Cliente")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Cliente")]
        public string id_cliente { get; set; }

        [Required(ErrorMessage = "Escriba el ID Empleado")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Empleado")]
        public string id_empleado { get; set; }

    }
}