using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class MarcaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MarcaController(ApplicationDbContext context)
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
            string cad_sql = "exec ListarMarcas";
            var arr_marca = _context.Marca.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            return Json(new { data = arr_marca });
        }


        [HttpGet]
        public JsonResult Consultar(String id_marca)
        {
            String cad_sql = "exec consultarMarca @id_marca";

            Marca marca = new Marca();

            marca = _context.Marca.FromSqlRaw(cad_sql, new SqlParameter("@id_marca", id_marca)).ToList().FirstOrDefault();

            return Json(marca);
        }

        [HttpPost]
        public IActionResult Grabar([FromBody] Marca marca)
        {
            bool rpta = true;

            try
            {
                Marca tmp_marca = null;

                tmp_marca = (from mar in _context.Marca
                                 where mar.id_marca == marca.id_marca
                                 select mar).FirstOrDefault();

                if (tmp_marca == null)
                {
                    _context.Marca.Add(marca);
                    _context.SaveChanges();
                }
                else
                {
                    tmp_marca.nombre = marca.nombre;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

        public JsonResult Borrar(string id_marca)
        {
            bool rpta = true;

            try
            {
                Marca marca = new Marca();

                marca = (from mar in _context.Marca
                             where mar.id_marca == id_marca
                             select mar).FirstOrDefault();

                _context.Marca.Remove(marca);
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
