using EShop.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace EShop.Model
{
    ///<inheritdoc/>
    /// <summary>
    /// Represent class for creating database
    /// </summary>
    /// <remarks>
    /// ShopContext inherit DbContext that allows us to use object relations mapper
    ///
    /// <para>Contains DbSet that will create table in database for passed type</para>
    /// </remarks>
    public class ShopContext : DbContext
    {
       /// <value>Represent Dbset for Customer
       /// <para>This will create Customer and Address as one table in database because it is 1 to 1 relation</para>
       /// </value>
     
        public DbSet<Customer> Customer { get; set; }
        /// <value>Represent Dbset for Book
        /// </value>
        public DbSet<Book> Book { get; set; }
        /// <value>Represent Dbset for Order
        /// </value>
        /// 
        public DbSet<Order> Order { get; set; }

        /// <value>Represent Dbset for Genre
        /// </value>
        /// 
        public DbSet<Genre> Genre { get; set; }
        /// <value>Represent Dbset for Autor
        /// </value>
        /// 
        public DbSet<Autor> Autor { get; set; }

        /// <summary>
        /// This will configure server to the database
        /// </summary>
        /// <param name="optionsBuilder"></param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EShop;");


        }
       
       
       /// <summary>
       /// Used for bulding model in database
       /// </summary>
       /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LegalEntity>().ToTable("Legal entity");

            modelBuilder.Entity<NaturalPerson>().ToTable("Natural person");
   
            modelBuilder.Entity<Customer>().OwnsOne(c => c.Address);

            modelBuilder.Entity<Order>().OwnsMany(o => o.OrderItems);

            modelBuilder.Entity<Book>().HasCheckConstraint("NotLessThenZero", "[Supplies] >= 0");
        }
    }
}
