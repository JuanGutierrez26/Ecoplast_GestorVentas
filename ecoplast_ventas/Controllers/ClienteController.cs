using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClienteController(ApplicationDbContext context)
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
            string cad_sql = "exec ListarClientes";
            var arr_cliente = _context.Cliente.FromSqlRaw(cad_sql).AsEnumerable().ToList();

            return Json(new { data = arr_cliente });
        }

        [HttpGet]
        public JsonResult Consultar(String id_cliente)
        {
            String cad_sql = "exec consultarCliente @id_cliente";

            Cliente cliente = new Cliente();

            cliente = _context.Cliente.FromSqlRaw(cad_sql, new SqlParameter("@id_cliente", id_cliente)).ToList().FirstOrDefault();

            return Json(cliente);
        }

        [HttpPost]
        public IActionResult Grabar([FromBody] Cliente cliente)
        {
            bool rpta = true;

            try
            {
                Cliente tmp_cliente = _context.Cliente
                    .FirstOrDefault(c => c.id_cliente == cliente.id_cliente);

                if (tmp_cliente == null)
                {
                    // Asegúrate de no asignar id_cliente si es generado por la base de datos
                    _context.Cliente.Add(cliente);
                }
                else
                {
                    tmp_cliente.nombre = cliente.nombre;
                    tmp_cliente.email = cliente.email;
                    tmp_cliente.telefono = cliente.telefono;
                    tmp_cliente.direccion = cliente.direccion;
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                rpta = false;
            }

            return Json(new { resultado = rpta });
        }
        public JsonResult Borrar(string id_cliente)
        {
            bool rpta = true;

            try
            {
                Cliente cliente = new Cliente();

                cliente = (from cat in _context.Cliente
                           where cat.id_cliente == id_cliente
                           select cat).FirstOrDefault();

                _context.Cliente.Remove(cliente);
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