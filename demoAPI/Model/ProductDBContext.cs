using Microsoft.EntityFrameworkCore;
namespace demoAPI.Model
{
    public class ProductDBContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            string conn = Global.getConnectString();
            opt.UseSqlServer(conn);

        }
    }
}
