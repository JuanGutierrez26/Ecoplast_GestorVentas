using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Proveedores")]
    public class Proveedor
    {

        [Key]
        [Required(ErrorMessage = "Ingrese un ID")]
        [StringLength(5)]
        [Display(Name = "ID")]
        public string id_proveedor { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Telefono")]
        [StringLength(20)]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Ingrese Direccion")]
        [StringLength(250)]
        [Display(Name = "Direccion")]
        public string direccion { get; set; }


    }
}
