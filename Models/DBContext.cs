using Microsoft.EntityFrameworkCore;

namespace ITDB.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<DBOrderDetail> DBOrderDetail { get; set; }
        public DbSet<DBPeriods> DBPeriods { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<RechargeRecords> RechargeRecords { get; set; }
        public DbSet<ShippingAddress> ShippingAddress { get; set; }
        public DbSet<ShowOrder> ShowOrder { get; set; }
        public DbSet<UserOtherLogin> UserOtherLogin { get; set; }
        public DbSet<ShopCart> ShopCart { get; set; }
        public DbSet<ConfigureCategory> ConfigureCategory { get; set; }
        public DbSet<ConfigurePosition> ConfigurePosition { get; set; }
        public DbSet<ConfigureSlider> ConfigureSlider { get; set; }

        public DbSet<TodoItem> TodoItems { get; set; }

        

    }
}