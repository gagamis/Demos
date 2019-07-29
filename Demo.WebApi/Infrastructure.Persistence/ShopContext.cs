using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Persistence
{
    /// <summary>
    /// Application DbContext class
    /// </summary>
    public class ShopContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        private readonly string _connectionString;

        public ShopContext(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            this._connectionString = connectionString;
        }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopContext).Assembly); // Separate Configuration Classes

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // validate options
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString); // if not configured yet, we have to use connection string

            base.OnConfiguring(optionsBuilder);
        }
    }
}
