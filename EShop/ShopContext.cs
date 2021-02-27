using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Model
{
    public class ShopContext : DbContext
    {

        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EShop;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalEntity>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Legal entity");
            });

            modelBuilder.Entity<NaturalPerson>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Natural person");
            });
        }
    }
}
