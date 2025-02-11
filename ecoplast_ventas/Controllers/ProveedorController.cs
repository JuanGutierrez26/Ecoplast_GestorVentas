using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class ProveedorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedorController(ApplicationDbContext context)
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
            string cad_sql = "exec ListarProveedores";
            var arr_proveedor = _context.Proveedor.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            return Json(new { data = arr_proveedor });
        }

        [HttpGet]
        public JsonResult Consultar(String id_proveedor)
        {
            String cad_sql = "exec consultarProveedor @id_proveedor";

            Proveedor proveedor = new Proveedor();

            proveedor = _context.Proveedor.FromSqlRaw(cad_sql, new SqlParameter("@id_proveedor", id_proveedor)).ToList().FirstOrDefault();

            return Json(proveedor);
        }

        [HttpPost]
        public IActionResult Grabar([FromBody] Proveedor proveedor)
        {
            bool rpta = true;

            try
            {
                Proveedor tmp_proveedor = null;

                tmp_proveedor = (from pd in _context.Proveedor
                                 where pd.id_proveedor == proveedor.id_proveedor
                                 select pd).FirstOrDefault();

                if (tmp_proveedor == null)
                {
                    _context.Proveedor.Add(proveedor);
                    _context.SaveChanges();
                }
                else
                {
                    tmp_proveedor.nombre = proveedor.nombre;
                    tmp_proveedor.telefono = proveedor.telefono;
                    tmp_proveedor.direccion = proveedor.direccion;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

        public JsonResult Borrar(string id_proveedor)
        {
            bool rpta = true;

            try
            {
                Proveedor proveedor = new Proveedor();

                proveedor = (from pd in _context.Proveedor
                             where pd.id_proveedor == id_proveedor
                             select pd).FirstOrDefault();

                _context.Proveedor.Remove(proveedor);
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
