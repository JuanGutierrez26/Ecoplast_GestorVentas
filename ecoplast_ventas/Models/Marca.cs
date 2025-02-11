using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("Marca")]
    public class Marca
    {
        [Key]
        [Required(ErrorMessage = "Escriba el ID Marca")]
        [StringLength(5)]
        [Display(Name = "Marca ID")]
        public string id_marca { get; set; }

        [Required(ErrorMessage = "Escriba nombre")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public String nombre { get; set; }
    }
}