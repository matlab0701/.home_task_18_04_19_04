﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250425101558_Setraints")]
    partial class Setraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Courier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CurrentLocation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TransportType")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Couriers");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PreparationTime")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourierId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeliveredAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentMethod")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CourierId");

                    b.HasIndex("RestaurantId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MenuItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<string>("SpecialInstructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MenuItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("DeliveryPrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<decimal>("MinOrderAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("Rating")
                        .HasColumnType("numeric");

                    b.Property<string>("WorkingHours")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Courier", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithOne("Courier")
                        .HasForeignKey("Domain.Entities.Courier", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.HasOne("Domain.Entities.Restaurant", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.HasOne("Domain.Entities.Courier", "Courier")
                        .WithMany()
                        .HasForeignKey("CourierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Restaurant", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Courier");

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Domain.Entities.Menu", "MenuItem")
                        .WithMany("OrderDetails")
                        .HasForeignKey("MenuItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuItem");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Domain.Entities.Restaurant", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Courier")
                        .IsRequired();

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
