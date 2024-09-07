using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Entity
{
    public class FreshKit:DbContext
    {
        public FreshKit(DbContextOptions<FreshKit> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Shirsts> Shakes { get; set; }
    }
}
