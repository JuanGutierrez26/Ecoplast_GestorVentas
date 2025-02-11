using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ecoplast_ventas.Controllers
{
    public class VentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Listar()
        {
            // Ejecuta la consulta para obtener las ventas
            string cad_sql = "exec ListarVentas";
            var arr_venta = _context.Venta.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener las clientes
            string cad_sql2 = "exec ListarClientes";
            var arr_cl = _context.Cliente.FromSqlRaw(cad_sql2).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener los proveedores
            string cad_sql3 = "exec ListarEmpleado";
            var arr_emp = _context.Empleado.FromSqlRaw(cad_sql3).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener los productos
            string cad_sql4 = "exec ListarProductos";
            var arr_pro = _context.Producto.FromSqlRaw(cad_sql4).AsEnumerable().ToList();

            string cad_sql5 = "exec ObtenerDetalleVentaMasAlto";
            var id_detalle_mas_alto = _context.DetalleVentaId
                                   .FromSqlRaw(cad_sql5)
                                   .AsEnumerable()
                                   .FirstOrDefault();

            // Devuelve todos los conjuntos de datos en un solo objeto JSON
            return Json(new { data = arr_venta, data2 = arr_cl, data3 = arr_emp, data4 = arr_pro, data5 = id_detalle_mas_alto });
        }


        //  ========================LISTAR DETALLE VENTA PARA PDF Y EXCEL ===================================0
        [HttpGet]
        public JsonResult ListarDetalleVenta(string id_venta)
        {
            // Ejecuta el procedimiento almacenado pasando el id_venta como parámetro
            var arr_listardv = _context.Detalle_Venta
                                        .FromSqlRaw("EXEC ObtenerDetallesPorVenta @id_venta = {0}", id_venta)
                                        .AsEnumerable()
                                        .ToList();

            // Devuelve los datos filtrados por id_venta en un objeto JSON
            return Json(new { data = arr_listardv });
        }



        [HttpPost]
        public IActionResult GrabarVenta([FromBody] Venta venta)
        {
            bool rpta = true;
            string mensaje = "Operación exitosa";
            Venta resultadoVenta = null;

            try
            {
                if (venta == null || string.IsNullOrEmpty(venta.id_venta) || venta.fecha == DateTime.MinValue || string.IsNullOrEmpty(venta.id_cliente) || string.IsNullOrEmpty(venta.id_empleado))
                {
                    mensaje = "Faltan valores en los campos.";
                    return Json(new { resultado = false, mensaje });
                }

                // Insertar la venta en la base de datos
                var nuevaVenta = new Venta
                {
                    id_venta = venta.id_venta,
                    fecha = venta.fecha,  // Asignamos la fecha directamente
                    id_cliente = venta.id_cliente,
                    id_empleado = venta.id_empleado
                };

                _context.Venta.Add(nuevaVenta);
                _context.SaveChanges();

                // Almacenar el resultado para devolverlo
                resultadoVenta = nuevaVenta;
            }
            catch (Exception ex)
            {
                rpta = false;
                mensaje = "Ocurrió un error: " + ex.Message;
            }

            return Json(new
            {
                resultado = rpta,
                mensaje,
                id_venta = resultadoVenta?.id_venta,
                id_cliente = resultadoVenta?.id_cliente,
                fecha = resultadoVenta?.fecha.ToString("yyyy-MM-dd")
            });
        }


        [HttpPost]
        public IActionResult GrabarDetalleVenta([FromBody] DetalleVentaRequest detalleVentaRequest)
        {
            bool rpta = true;
            string mensaje = "Operación exitosa";

            try
            {
                // Validar que la venta ya existe (por si acaso)
                var ventaExistente = _context.Venta.Any(v => v.id_venta == detalleVentaRequest.id_venta);
                if (!ventaExistente)
                {
                    mensaje = "La venta asociada no existe.";
                    return Json(new { resultado = false, mensaje });
                }

                // Guardar los detalles de la venta
                foreach (var producto in detalleVentaRequest.productos)
                {
                    var detalleVenta = new Detalle_Venta
                    {
                        id_venta = detalleVentaRequest.id_venta,  // Usar el id de venta existente
                        id_producto = producto.id_producto,
                        cantidad = producto.cantidad,
                        precio_unitario = producto.precio_unitario
                    };

                    _context.Detalle_Venta.Add(detalleVenta);
                }

                _context.SaveChanges(); // Guardar los detalles de la venta
            }
            catch (Exception ex)
            {
                rpta = false;
                mensaje = "Ocurrió un error: " + ex.Message;
            }

            return Json(new { resultado = rpta, mensaje });
        }

        // DTO para recibir los detalles de venta
        public class DetalleVentaRequest
        {
            public string id_venta { get; set; }
            public List<DetalleVentaProducto> productos { get; set; }
        }

        // DTO para cada producto
        public class DetalleVentaProducto
        {
            public string id_producto { get; set; }
            public int cantidad { get; set; }
            public decimal precio_unitario { get; set; }
        }

        // =================================== CODIGO DE EDITAR ===================================
        public JsonResult ObtenerVenta(string id_venta)
        {
            // Obtener la venta junto con sus detalles si es necesario
            var venta = _context.Venta
                .Where(v => v.id_venta == id_venta)
                .FirstOrDefault();

            if (venta != null)
            {
                // Opcional: si necesitas más datos (por ejemplo, los detalles de la venta)
                var detallesVenta = _context.Detalle_Venta
                    .Where(dv => dv.id_venta == id_venta)
                    .ToList();

                // Retornar los datos de la venta
                return Json(new
                {
                    id_venta = venta.id_venta,
                    fecha = venta.fecha.ToString("yyyy-MM-dd"),  // Ajustar formato si es necesario
                    id_cliente = venta.id_cliente,
                    id_empleado = venta.id_empleado
                });
            }

            return Json(null);  // Retorna null si no se encuentra la venta
        }

        public JsonResult ObtenerDetalleVenta(string id_venta)
        {
            // Obtener los detalles de la venta con la id_venta proporcionada
            var detalleVenta = _context.Detalle_Venta
                .Where(d => d.id_venta == id_venta)
                .Select(d => new
                {
                    d.id_detalle,
                    d.id_venta,
                    d.id_producto,
                    d.cantidad,
                    d.precio_unitario
                })
                .ToList();

            return Json(detalleVenta);
        }


        // ================================ ELIMINAR =========================================

        public JsonResult Borrar(string id_venta)
        {
            bool rpta = true;

            try
            {
                // Obtener la venta y los detalles asociados
                Venta venta = _context.Venta
                    .FirstOrDefault(vt => vt.id_venta == id_venta);

                if (venta != null)
                {
                    // Eliminar los detalles asociados a la venta
                    var detallesVenta = _context.Detalle_Venta
                        .Where(dv => dv.id_venta == id_venta).ToList();

                    _context.Detalle_Venta.RemoveRange(detallesVenta);  // Eliminar todos los detalles

                    // Eliminar la venta
                    _context.Venta.Remove(venta);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

    }
}
