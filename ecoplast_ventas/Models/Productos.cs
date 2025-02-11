using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Productos")]

    public class Producto
    {
        [Key]
        [Required(ErrorMessage = "Escriba el ID Productos")]
        [MinLength(6, ErrorMessage = "Escriba 6 dígitos")]
        [Display(Name = "Productos ID")]
        public string id_producto { get; set; } 

        [Required(ErrorMessage = "Escriba nombre")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }

        [Required(ErrorMessage = "Escriba la descripción")]
        [StringLength(200)]  // como es descripcion se puede quitar el limite de caracteres
        [Display(Name = "Descripción")]
        public String descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese precio")]
        [Display(Name = "Precio")]
        public decimal precio { get; set; } // cambie String por decimal

        [Required(ErrorMessage = "Ingrese el Stock")]
        [Display(Name = "Stock")]
        public int stock { get; set; } // cambie String por int

        [Required(ErrorMessage = "Escriba el ID Marca")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Marca")]
        public string id_marca { get; set; } // cambie String por int

        [Required(ErrorMessage = "Escriba el ID Proveedor")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Proveedor")]
        public string id_proveedor { get; set; } // cambie String por int

        [Required(ErrorMessage = "Escriba el ID Categoria")]
        [MinLength(5, ErrorMessage = "Escriba 5 dígitos")]
        [Display(Name = "Categoria")]
        public string id_categoria { get; set; } // cambie String por int
    }
}