using ecoplast_ventas.Data;
using ecoplast_ventas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecoplast_ventas.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
