﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.11");

            modelBuilder.Entity("Domain.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Director")
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .HasColumnType("TEXT");

                    b.Property<double>("IMDBScore")
                        .HasColumnType("REAL");

                    b.Property<string>("ImageURL")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Is3D")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsIMAX")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Length")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.DeliveryMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DeliveryTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("DeliveryMethods");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("DeliveryMethodId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("TEXT");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryMethodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.ScreeningRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Is3DRoom")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsIMAXRoom")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ScreeningRooms");
                });

            modelBuilder.Entity("Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("TEXT");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EndTime")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ScreeningRoomId")
                        .HasColumnType("TEXT");

                    b.Property<int>("StarTime")
                        .HasColumnType("INTEGER");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("ScreeningRoomId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.HasOne("Domain.Entities.OrderAggregate.DeliveryMethod", "DeliveryMethod")
                        .WithMany()
                        .HasForeignKey("DeliveryMethodId");

                    b.OwnsOne("Domain.Entities.OrderAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("City")
                                .HasColumnType("TEXT");

                            b1.Property<string>("State")
                                .HasColumnType("TEXT");

                            b1.Property<string>("Street")
                                .HasColumnType("TEXT");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("TEXT");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("DeliveryMethod");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.HasOne("Domain.Entities.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Domain.Entities.OrderAggregate.OrderedItem", "OrderedItem", b1 =>
                        {
                            b1.Property<Guid>("OrderItemId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("ImageUrl")
                                .HasColumnType("TEXT");

                            b1.Property<string>("MovieTitle")
                                .HasColumnType("TEXT");

                            b1.Property<string>("ScreeningRoomName")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("TEXT");

                            b1.Property<int>("SessionStartTime")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("TicketType")
                                .HasColumnType("INTEGER");

                            b1.HasKey("OrderItemId");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemId");
                        });

                    b.Navigation("OrderedItem");
                });

            modelBuilder.Entity("Domain.Entities.Session", b =>
                {
                    b.HasOne("Domain.Entities.Movie", "Movie")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.ScreeningRoom", "ScreeningRoom")
                        .WithMany("Sessions")
                        .HasForeignKey("ScreeningRoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("ScreeningRoom");
                });

            modelBuilder.Entity("Domain.Entities.Movie", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.ScreeningRoom", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
