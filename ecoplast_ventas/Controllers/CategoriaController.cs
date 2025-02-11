using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriaController(ApplicationDbContext context)
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
            string cad_sql = "exec ListarCategorias";
            var arr_categoria = _context.Categoria.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            return Json(new { data = arr_categoria });
        }


        [HttpGet]
        public JsonResult Consultar(String id_categoria)
        {
            String cad_sql = "exec consultarCategoria @id_categoria";

            Categoria categoria = new Categoria();

            categoria = _context.Categoria.FromSqlRaw(cad_sql, new SqlParameter("@id_categoria", id_categoria)).ToList().FirstOrDefault();

            return Json(categoria);
        }

        [HttpPost]
        public IActionResult Grabar([FromBody] Categoria categoria)
        {
            bool rpta = true;

            try
            {
                Categoria tmp_categoria = null;

                tmp_categoria = (from cat in _context.Categoria
                                 where cat.id_categoria == categoria.id_categoria
                                 select cat).FirstOrDefault();

                if (tmp_categoria == null)
                {
                    _context.Categoria.Add(categoria);
                    _context.SaveChanges();
                }
                else
                {
                    tmp_categoria.nombre = categoria.nombre;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

        public JsonResult Borrar(string id_categoria)
        {
            bool rpta = true;

            try
            {
                Categoria categoria = new Categoria();

                categoria = (from cat in _context.Categoria
                             where cat.id_categoria == id_categoria
                             select cat).FirstOrDefault();

                _context.Categoria.Remove(categoria);
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
