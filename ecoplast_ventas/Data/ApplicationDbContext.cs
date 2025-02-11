using ecoplast_ventas.Models;
using Microsoft.EntityFrameworkCore;

namespace ecoplast_ventas.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>().ToTable("Productos");
        }

        public DbSet<Proveedor> Proveedor { get; set; }

        public DbSet<Empleado> Empleado { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Categoria> Categoria { get; set; }

        public DbSet<Marca> Marca { get; set; }

        public DbSet<Producto> Producto { get; set; }

        public DbSet<Venta> Venta { get; set; }

        public DbSet<Detalle_Venta> Detalle_Venta { get; set; }

        public DbSet<DetalleVentaId> DetalleVentaId { get; set; }


    }
}
