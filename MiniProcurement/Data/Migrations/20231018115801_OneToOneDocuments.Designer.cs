﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniProcurement.Data.Contexts;

#nullable disable

namespace MiniProcurement.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231018115801_OneToOneDocuments")]
    partial class OneToOneDocuments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniProcurement.Data.Entities.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ManagerUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerUserId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.DocumentBase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.InvoiceDocument", b =>
                {
                    b.Property<int>("DocumentBaseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentCardNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("DocumentBaseId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.PurchaseRequestDocument", b =>
                {
                    b.Property<int>("DocumentBaseId")
                        .HasColumnType("int");

                    b.Property<string>("DeliveryAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocumentBaseId");

                    b.ToTable("PurchaseRequests");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.PurchaseRequestDocumentItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemStatus")
                        .HasColumnType("int");

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("PurchaseRequestDocumentId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PurchaseRequestDocumentId");

                    b.ToTable("PurchaseRequestDocumentItems");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.Department", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerUserId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.DocumentBase", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.User", "CreatedBy")
                        .WithMany("Documents")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.InvoiceDocument", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.DocumentBase", "DocumentBase")
                        .WithOne("InvoiceRequest")
                        .HasForeignKey("MiniProcurement.Data.Entities.InvoiceDocument", "DocumentBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentBase");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.PurchaseRequestDocument", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.DocumentBase", "DocumentBase")
                        .WithOne("PurchaseRequest")
                        .HasForeignKey("MiniProcurement.Data.Entities.PurchaseRequestDocument", "DocumentBaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentBase");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.PurchaseRequestDocumentItem", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.PurchaseRequestDocument", "PurchaseRequestDocument")
                        .WithMany("PurchaseRequestDocumentItems")
                        .HasForeignKey("PurchaseRequestDocumentId");

                    b.Navigation("PurchaseRequestDocument");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.User", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("MiniProcurement.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniProcurement.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.DocumentBase", b =>
                {
                    b.Navigation("InvoiceRequest");

                    b.Navigation("PurchaseRequest");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.PurchaseRequestDocument", b =>
                {
                    b.Navigation("PurchaseRequestDocumentItems");
                });

            modelBuilder.Entity("MiniProcurement.Data.Entities.User", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
