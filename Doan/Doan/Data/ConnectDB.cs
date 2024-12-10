using Doan.Models;
using Microsoft.EntityFrameworkCore;

namespace Doan.Data
{
    public class ConnectDB : DbContext
    {

        public ConnectDB(DbContextOptions<ConnectDB> options) : base(options) { }

        public DbSet<Category> categorys { get; set; }

        public DbSet<Product> products { get; set; }

        public DbSet<Order> orders { get; set; }

        public DbSet<OrderDetail> orderDetails { get; set; }

        public DbSet<Image> images { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Discount> discounts { get; set; }




    }
}
