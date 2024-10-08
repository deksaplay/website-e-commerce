﻿using website_e_commerce.Models;
using Microsoft.EntityFrameworkCore;


namespace website_e_commerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category>Categories{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Review> Reviews { get; set; }



    }
}