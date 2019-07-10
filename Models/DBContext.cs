using Microsoft.EntityFrameworkCore;

namespace ITDB.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<DBOrderDetail> DBOrderDetails { get; set; }
        public DbSet<DBPeriods> DBPeriods { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<RechargeRecords> RechargeRecords { get; set; }
        public DbSet<ShippingAddress> ShippingAddress { get; set; }
        public DbSet<ShowOrder> ShowOrders { get; set; }
        public DbSet<UserOtherLogin> UserOtherLogins { get; set; }
        public DbSet<ShopCart> ShopCarts { get; set; }
        public DbSet<ConfigureCategory> ConfigureCategories { get; set; }
        public DbSet<ConfigurePosition> ConfigurePositions { get; set; }
        public DbSet<ConfigureSlider> ConfigureSliders { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        
    }
    
}