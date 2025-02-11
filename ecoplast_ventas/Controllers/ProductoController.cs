using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
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
            // Ejecuta la consulta para obtener los productos
            string cad_sql = "exec ListarProductos";
            var arr_producto = _context.Producto.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener las marcas
            string cad_sql2 = "exec ListarMarcas";
            var arr_marca = _context.Marca.FromSqlRaw(cad_sql2).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener los proveedores
            string cad_sql3 = "exec ListarProveedores";
            var arr_pro = _context.Proveedor.FromSqlRaw(cad_sql3).AsEnumerable().ToList();

            // Ejecuta la consulta para obtener las categorías
            string cad_sql4 = "exec ListarCategorias";
            var arr_cad = _context.Categoria.FromSqlRaw(cad_sql4).AsEnumerable().ToList();

            // Devuelve todos los conjuntos de datos en un solo objeto JSON
            return Json(new { data = arr_producto, data2 = arr_marca, data3 = arr_pro, data4 = arr_cad });
        }



        [HttpGet]
        public JsonResult Consultar(String id_producto)
        {
            String cad_sql = "exec consultarProducto @id_producto";

            Producto producto = new Producto();

            producto = _context.Producto.FromSqlRaw(cad_sql, new SqlParameter("@id_producto", id_producto)).ToList().FirstOrDefault();

            return Json(producto);
        }


        [HttpPost]
        public IActionResult Grabar([FromBody] Producto producto)
        {
            bool rpta = true;
            string mensaje = "Operación exitosa";

            try
            {
                // Verificar los valores recibidos
                if (producto == null ||
                    string.IsNullOrEmpty(producto.id_producto) ||
                    string.IsNullOrEmpty(producto.nombre) ||
                    string.IsNullOrEmpty(producto.descripcion) ||
                    producto.precio == null ||
                    producto.stock == null ||
                    string.IsNullOrEmpty(producto.id_marca) ||
                    string.IsNullOrEmpty(producto.id_proveedor) ||
                    string.IsNullOrEmpty(producto.id_categoria))
                {
                    mensaje = "Faltan valores en los campos.";
                    return Json(new { resultado = false, mensaje });
                }

                // Ejecutar el procedimiento almacenado
                var result = _context.Database.ExecuteSqlRaw(
                    "EXEC guardarProducto @id_producto, @nombre, @descripcion, @precio, @stock, @id_marca, @id_proveedor, @id_categoria",
                    new SqlParameter("@id_producto", producto.id_producto),
                    new SqlParameter("@nombre", producto.nombre),
                    new SqlParameter("@descripcion", producto.descripcion),
                    new SqlParameter("@precio", producto.precio),
                    new SqlParameter("@stock", producto.stock),
                    new SqlParameter("@id_marca", producto.id_marca),
                    new SqlParameter("@id_proveedor", producto.id_proveedor),
                    new SqlParameter("@id_categoria", producto.id_categoria)
                );
            }
            catch (Exception ex)
            {
                rpta = false;
                mensaje = "Ocurrió un error: " + ex.Message;
            }

            return Json(new { resultado = rpta, mensaje });
        }



        public JsonResult Borrar(string id_producto)
        {
            bool rpta = true;

            try
            {
                Producto producto = new Producto();

                producto = (from pro in _context.Producto
                             where pro.id_producto == id_producto
                             select pro).FirstOrDefault();

                _context.Producto.Remove(producto);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

    }
}
