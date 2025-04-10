﻿// <auto-generated />
using System;
using Food.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Food.Core.Migrations
{
    [DbContext(typeof(FoodDeliveryContext))]
    [Migration("20250408080322_RunnerHasData")]
    partial class RunnerHasData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Food.Core.Model.MenuItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Food.Core.Model.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DeliveryInstructions")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Food.Core.Model.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("Food.Core.Model.Runner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActiveOrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ActiveOrderId");

                    b.ToTable("Runners");

                    b.HasData(
                        new
                        {
                            Id = 1
                        },
                        new
                        {
                            Id = 2
                        },
                        new
                        {
                            Id = 3
                        });
                });

            modelBuilder.Entity("Food.Core.Model.MenuItem", b =>
                {
                    b.HasOne("Food.Core.Model.Restaurant", null)
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Food.Core.Model.Order", b =>
                {
                    b.OwnsOne("Food.Core.Model.CustomerInfo", "CustomerInfo", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("PhoneNumber")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsOne("Food.Core.Model.OrderInfo", "OrderDetails", b1 =>
                        {
                            b1.Property<int>("OrderId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("ExtraInstructions")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("MenuItemId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("MenuItemName")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsOne("Food.Core.Model.Pricing", "Price", b2 =>
                                {
                                    b2.Property<int>("OrderInfoOrderId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<decimal>("serviceFee")
                                        .HasColumnType("TEXT");

                                    b2.Property<decimal>("sum")
                                        .HasColumnType("TEXT");

                                    b2.Property<decimal>("total")
                                        .HasColumnType("TEXT");

                                    b2.Property<decimal>("unit")
                                        .HasColumnType("TEXT");

                                    b2.HasKey("OrderInfoOrderId");

                                    b2.ToTable("Orders");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderInfoOrderId");
                                });

                            b1.Navigation("Price")
                                .IsRequired();
                        });

                    b.Navigation("CustomerInfo")
                        .IsRequired();

                    b.Navigation("OrderDetails")
                        .IsRequired();
                });

            modelBuilder.Entity("Food.Core.Model.Restaurant", b =>
                {
                    b.OwnsOne("Food.Core.Model.OpenHours", "OpenHours", b1 =>
                        {
                            b1.Property<int>("RestaurantId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Hours")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("RestaurantId");

                            b1.ToTable("Restaurants");

                            b1.WithOwner()
                                .HasForeignKey("RestaurantId");
                        });

                    b.Navigation("OpenHours")
                        .IsRequired();
                });

            modelBuilder.Entity("Food.Core.Model.Runner", b =>
                {
                    b.HasOne("Food.Core.Model.Order", null)
                        .WithMany()
                        .HasForeignKey("ActiveOrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
