﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
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
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Director")
                        .HasColumnType("text");

                    b.Property<string>("Genre")
                        .HasColumnType("text");

                    b.Property<double>("IMDBScore")
                        .HasColumnType("double precision");

                    b.Property<string>("ImageURL")
                        .HasColumnType("text");

                    b.Property<bool>("Is3D")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsIMAX")
                        .HasColumnType("boolean");

                    b.Property<int>("Length")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.DeliveryMethod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeliveryTime")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("DeliveryMethods");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeliveryMethodId")
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryMethodId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Domain.Entities.OrderAggregate.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Domain.Entities.ScreeningRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Is3DRoom")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsIMAXRoom")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("ScreeningRooms");
                });

            modelBuilder.Entity("Domain.Entities.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("numeric");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EndTime")
                        .HasColumnType("integer");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScreeningRoomId")
                        .HasColumnType("uuid");

                    b.Property<int>("StarTime")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

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
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .HasColumnType("text");

                            b1.Property<string>("Street")
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .HasColumnType("text");

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
                                .HasColumnType("uuid");

                            b1.Property<string>("ImageUrl")
                                .HasColumnType("text");

                            b1.Property<string>("MovieTitle")
                                .HasColumnType("text");

                            b1.Property<string>("ScreeningRoomName")
                                .HasColumnType("text");

                            b1.Property<Guid>("SessionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("SessionStartTime")
                                .HasColumnType("integer");

                            b1.Property<int>("TicketType")
                                .HasColumnType("integer");

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
