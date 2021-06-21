﻿// <auto-generated />
using System;
using EShop.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EShop.Model.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20210508142304_ini")]
    partial class ini
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutorBook", b =>
                {
                    b.Property<int>("AutorsAutorId")
                        .HasColumnType("int");

                    b.Property<int>("BooksBookId")
                        .HasColumnType("int");

                    b.HasKey("AutorsAutorId", "BooksBookId");

                    b.HasIndex("BooksBookId");

                    b.ToTable("AutorBook");
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.Property<int>("BooksBookId")
                        .HasColumnType("int");

                    b.Property<int>("GenresGenreId")
                        .HasColumnType("int");

                    b.HasKey("BooksBookId", "GenresGenreId");

                    b.HasIndex("GenresGenreId");

                    b.ToTable("BookGenre");
                });

            modelBuilder.Entity("EShop.Model.Domain.Autor", b =>
                {
                    b.Property<int>("AutorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AutorId");

                    b.ToTable("Autor");
                });

            modelBuilder.Entity("EShop.Model.Domain.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Supplies")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Book");

                    b.HasCheckConstraint("NotLessThenZero", "[Supplies] >= 0");
                });

            modelBuilder.Entity("EShop.Model.Domain.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<long>("VerificationCode")
                        .HasColumnType("bigint");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("EShop.Model.Domain.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("EShop.Model.Domain.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("EShop.Model.Domain.LegalEntity", b =>
                {
                    b.HasBaseType("EShop.Model.Domain.Customer");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TIN")
                        .HasColumnType("int");

                    b.ToTable("Legal entity");
                });

            modelBuilder.Entity("EShop.Model.Domain.NaturalPerson", b =>
                {
                    b.HasBaseType("EShop.Model.Domain.Customer");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Natural person");
                });

            modelBuilder.Entity("AutorBook", b =>
                {
                    b.HasOne("EShop.Model.Domain.Autor", null)
                        .WithMany()
                        .HasForeignKey("AutorsAutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EShop.Model.Domain.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookGenre", b =>
                {
                    b.HasOne("EShop.Model.Domain.Book", null)
                        .WithMany()
                        .HasForeignKey("BooksBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EShop.Model.Domain.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Model.Domain.Customer", b =>
                {
                    b.OwnsOne("EShop.Model.Domain.Address", "Address", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("CityName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("PTT")
                                .HasColumnType("int");

                            b1.Property<string>("StreetName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customer");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("EShop.Model.Domain.Order", b =>
                {
                    b.HasOne("EShop.Model.Domain.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.OwnsMany("EShop.Model.Domain.OrderItem", "OrderItems", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("int");

                            b1.Property<int>("OrderItemId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int?>("BookId")
                                .HasColumnType("int");

                            b1.Property<int>("Quantity")
                                .HasColumnType("int");

                            b1.HasKey("OrderId", "OrderItemId");

                            b1.HasIndex("BookId");

                            b1.ToTable("OrderItem");

                            b1.HasOne("EShop.Model.Domain.Book", "Book")
                                .WithMany()
                                .HasForeignKey("BookId");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.Navigation("Book");
                        });

                    b.Navigation("Customer");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("EShop.Model.Domain.LegalEntity", b =>
                {
                    b.HasOne("EShop.Model.Domain.Customer", null)
                        .WithOne()
                        .HasForeignKey("EShop.Model.Domain.LegalEntity", "CustomerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Model.Domain.NaturalPerson", b =>
                {
                    b.HasOne("EShop.Model.Domain.Customer", null)
                        .WithOne()
                        .HasForeignKey("EShop.Model.Domain.NaturalPerson", "CustomerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EShop.Model.Domain.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
