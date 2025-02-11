using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("empleado")]

    public class Empleado
    {
        [Key]
        [Required(ErrorMessage = "Ingrese un ID")]
        [StringLength(5, ErrorMessage = "El nombre no puede exceder los 5 caracteres")]
        [Display(Name = "Empleado ID")]
        public string id_empleado { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Email")]
        [StringLength(100)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Ingrese Telefono")]
        [StringLength(9)]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

    }
}