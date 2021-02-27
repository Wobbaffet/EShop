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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EShop;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalEntity>().ToTable("Legal entity");
            modelBuilder.Entity<NaturalPerson>().ToTable("Natural person");
            modelBuilder.Entity<Address>().HasData(new Address() { AddressId = 1, PTT = 123 });
            modelBuilder.Entity<LegalEntity>().HasData(new LegalEntity() { CompanyName = "asd", CompanyNumber = "1234", CustomerId = 1, Email = "asdfgg", Password = "draga peder", PhoneNumber = "123", TIN = 123, AddressId = 1 });
            modelBuilder.Entity<Address>()
            .HasOne<Customer>(a => a.Customer)
            .WithOne(c => c.Address)
            .HasForeignKey<Customer>(c => c.AddressId);
        }
    }
}
