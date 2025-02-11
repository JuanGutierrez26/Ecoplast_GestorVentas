using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [Required(ErrorMessage = "Ingrese un ID")]
        [StringLength(5, ErrorMessage = "El nombre no puede exceder los 5 caracteres")]
        [Display(Name = "Categoria ID")]
        public string id_categoria { get; set; }

        [Required(ErrorMessage = "Escriba el nombre")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
    }
}
