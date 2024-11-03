﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RHWebApplication.Database;

#nullable disable

namespace RHWebApplication.Database.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241101140801_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RHWebApplication.Shared.Models.CompanyModels.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CNPJ")
                        .HasColumnType("int");

                    b.Property<string>("CorporateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies", (string)null);
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.CompanyModels.JobTitle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BaseSalary")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPericulosity")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OverTimeValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnhealthyLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompanyID");

                    b.ToTable("jobTitles");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.PayrollModels.Payroll", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Commission")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Deductions")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Gross")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("INSSDeduction")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("IRRFDeduction")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Net")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<float>("OverTime")
                        .HasColumnType("real");

                    b.Property<decimal>("OverTimeAditionals")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PericulosityValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("UnhealthyValue")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("PayrollHistory");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Users", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Admin", b =>
                {
                    b.HasBaseType("RHWebApplication.Shared.Models.UserModels.User");

                    b.ToTable("Admins", (string)null);
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Employee", b =>
                {
                    b.HasBaseType("RHWebApplication.Shared.Models.UserModels.User");

                    b.Property<int>("Dependents")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TerminationDate")
                        .HasColumnType("datetime2");

                    b.HasIndex("JobId");

                    b.ToTable("Employees", (string)null);
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Staff", b =>
                {
                    b.HasBaseType("RHWebApplication.Shared.Models.UserModels.User");

                    b.ToTable("Staffs", (string)null);
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.CompanyModels.JobTitle", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.CompanyModels.Company", "Company")
                        .WithMany("JobTitles")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.PayrollModels.Payroll", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.UserModels.Employee", "Employee")
                        .WithMany("PayrollHistory")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.User", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.CompanyModels.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Admin", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.UserModels.User", null)
                        .WithOne()
                        .HasForeignKey("RHWebApplication.Shared.Models.UserModels.Admin", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Employee", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.UserModels.User", null)
                        .WithOne()
                        .HasForeignKey("RHWebApplication.Shared.Models.UserModels.Employee", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RHWebApplication.Shared.Models.CompanyModels.JobTitle", "Job")
                        .WithMany("Employees")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Job");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Staff", b =>
                {
                    b.HasOne("RHWebApplication.Shared.Models.UserModels.User", null)
                        .WithOne()
                        .HasForeignKey("RHWebApplication.Shared.Models.UserModels.Staff", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.CompanyModels.Company", b =>
                {
                    b.Navigation("JobTitles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.CompanyModels.JobTitle", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("RHWebApplication.Shared.Models.UserModels.Employee", b =>
                {
                    b.Navigation("PayrollHistory");
                });
#pragma warning restore 612, 618
        }
    }
}