namespace ecoplast_ventas.Models
{
    public class DetalleVentaDTO
    {
        public string id_producto { get; set; }
        public int cantidad { get; set; }
        public decimal precio_unitario { get; set; }
    }

}
