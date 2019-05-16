using System;
using System.Collections.Generic;
using System.Text;
using ASP_Souvenirs.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ASP_Souvenirs.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //public ApplicationDbContext()
        //{
        //}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        //public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Souvenir> Sourvenirs { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Souvenir>().ToTable("Sourvenir");
            builder.Entity<OrderItem>().ToTable("OrderItem");
            builder.Entity<Order>().ToTable("Order");
            builder.Entity<Supplier>().ToTable("Supplier");
            builder.Entity<CartItem>().ToTable("CartItem");

            //builder.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany(o => o.OrderItems).OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Cascade);
        }

        //public DbSet<ASP_Souvenirs.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
