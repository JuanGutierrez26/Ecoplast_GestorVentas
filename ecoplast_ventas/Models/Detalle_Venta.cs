using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Detalle_Venta")]
    public class Detalle_Venta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincremento
        [Display(Name = "Detalle Venta ID")]
        public int id_detalle { get; set; }  // Cambié a int para autoincremento

        [Required(ErrorMessage = "Escriba el ID Venta")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Venta ID")]
        public string id_venta { get; set; }

        [Required(ErrorMessage = "Escriba el ID Producto")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Venta ID")]
        public string id_producto { get; set; }

        [Required(ErrorMessage = "Ingrese cantidad")]
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Required(ErrorMessage = "Ingrese precio unitario")]
        [Display(Name = "Precio unitario")]
        public decimal precio_unitario { get; set; }


    }
}
