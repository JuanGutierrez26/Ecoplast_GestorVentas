using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoController(ApplicationDbContext context)
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
            string cad_sql = "exec ListarEmpleado";
            var arr_empleado = _context.Empleado.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            return Json(new { data = arr_empleado });
        }


        [HttpGet]
        public JsonResult Consultar(String id_empleado)
        {
            String cad_sql = "exec consultarEmpleado @id_empleado";

            Empleado empleado = new Empleado();

            empleado = _context.Empleado.FromSqlRaw(cad_sql, new SqlParameter("@id_empleado", id_empleado)).ToList().FirstOrDefault();

            return Json(empleado);
        }

        [HttpPost]
        public IActionResult Grabar([FromBody] Empleado empleado)
        {
            bool rpta = true;

            try
            {
                Empleado tmp_empleado = null;

                tmp_empleado = (from emp in _context.Empleado
                                where emp.id_empleado == empleado.id_empleado
                                select emp).FirstOrDefault();

                if (tmp_empleado == null)
                {
                    _context.Empleado.Add(empleado);
                    _context.SaveChanges();
                }
                else
                {
                    tmp_empleado.nombre = empleado.nombre;
                    tmp_empleado.email = empleado.email;
                    tmp_empleado.telefono = empleado.telefono;

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }

        public JsonResult Borrar(string id_empleado)
        {
            bool rpta = true;

            try
            {
                Empleado empleado = new Empleado();

                empleado = (from emp in _context.Empleado
                            where emp.id_empleado == id_empleado
                            select emp).FirstOrDefault();

                _context.Empleado.Remove(empleado);
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