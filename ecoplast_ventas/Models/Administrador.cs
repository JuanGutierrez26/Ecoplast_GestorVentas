using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecoplast_ventas.Models
{
    [Table("administrador")]

    public class Administrador
    {
        [Key]
        [Required(ErrorMessage = "Ingrese un ID")]
        [Display(Name = "ID")]
        public int id_administrador { get; set; }

        [Required(ErrorMessage = "Ingrese Nombre")]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese Contraseña")]
        [StringLength(255)]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }

    }
}
