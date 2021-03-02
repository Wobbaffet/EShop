using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model
{
    public class ShopContext : DbContext
    {

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Order> Order { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EShop;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalEntity>().ToTable("Legal entity");
            modelBuilder.Entity<NaturalPerson>().ToTable("Natural person");

            modelBuilder.Entity<Address>()
            .HasOne<Customer>(a => a.Customer)
            .WithOne(c => c.Address)
            .HasForeignKey<Customer>(c => c.AddressId);

            modelBuilder.Entity<Order>().OwnsMany(o => o.OrderItems);

            modelBuilder.Entity<Book>().HasCheckConstraint("NotLessThenZero", "[Supplies] >= 0");
        }
    }
}
