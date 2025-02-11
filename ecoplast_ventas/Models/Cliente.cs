using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        [StringLength(6, ErrorMessage = "El nombre no puede exceder los 6 caracteres")]
        [Required(ErrorMessage = "Ingrese un ID")]
        [Display(Name = "ID")]
        public string id_cliente { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Email")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Ingrese Telefono")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        [Display(Name = "Telefono")]
        public string telefono { get; set; }

        [Required(ErrorMessage = "Ingrese Direccion")]
        [StringLength(255, ErrorMessage = "El nombre no puede exceder los 255 caracteres")]
        [Display(Name = "Direccion")]
        public string direccion { get; set; }
    }
}