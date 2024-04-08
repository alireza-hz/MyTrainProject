﻿// <auto-generated />
using System;
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(TrainDbContext))]
    [Migration("20240327025748_create")]
    partial class create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Products.Category", b =>
                {
                    b.Property<int>("MyCategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MyCategoryID"));

                    b.Property<DateTime>("Createdate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Isdelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MyCategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Products.Level", b =>
                {
                    b.Property<int>("MYlevelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MYlevelID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MYlevelID");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("Domain.Products.MyProduct", b =>
                {
                    b.Property<int>("MyProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MyProductId"));

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountVideo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Createdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DurationData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Isdelete")
                        .HasColumnType("bit");

                    b.Property<int>("MYlevelID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MyCategoryID")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MyProductId");

                    b.HasIndex("MYlevelID");

                    b.HasIndex("MyCategoryID");

                    b.ToTable("MyProducts");
                });

            modelBuilder.Entity("Domain.Shop.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("IdTransaction")
                        .HasColumnType("int");

                    b.Property<int>("MYUserID")
                        .HasColumnType("int");

                    b.Property<int>("MyProductId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("IdTransaction");

                    b.HasIndex("MYUserID");

                    b.HasIndex("MyProductId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domain.Shop.Transaction", b =>
                {
                    b.Property<int>("IdTransaction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTransaction"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("IdTransaction");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Domain.Users.MyRole", b =>
                {
                    b.Property<int>("MyRoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MyRoleID"));

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("RoleTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("MyRoleID");

                    b.ToTable("MyRoles");
                });

            modelBuilder.Entity("Domain.Users.MyUser", b =>
                {
                    b.Property<int>("MYUserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MYUserID"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ForMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MyRoleID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("MYUserID");

                    b.HasIndex("MyRoleID");

                    b.ToTable("MyUsers");
                });

            modelBuilder.Entity("Domain.Products.MyProduct", b =>
                {
                    b.HasOne("Domain.Products.Level", "Level")
                        .WithMany("Products")
                        .HasForeignKey("MYlevelID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Products.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("MyCategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("Domain.Shop.Item", b =>
                {
                    b.HasOne("Domain.Shop.Transaction", "Transaction")
                        .WithMany("Item")
                        .HasForeignKey("IdTransaction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.MyUser", "MyUser")
                        .WithMany("Item")
                        .HasForeignKey("MYUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Products.MyProduct", "MyProduct")
                        .WithMany("Item")
                        .HasForeignKey("MyProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyProduct");

                    b.Navigation("MyUser");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Domain.Users.MyUser", b =>
                {
                    b.HasOne("Domain.Users.MyRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("MyRoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Products.Category", b =>
                {
                    b.Navigation("products");
                });

            modelBuilder.Entity("Domain.Products.Level", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Products.MyProduct", b =>
                {
                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.Shop.Transaction", b =>
                {
                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.Users.MyRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Users.MyUser", b =>
                {
                    b.Navigation("Item");
                });
#pragma warning restore 612, 618
        }
    }
}
