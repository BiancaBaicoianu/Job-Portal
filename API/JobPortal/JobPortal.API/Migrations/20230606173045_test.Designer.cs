﻿// <auto-generated />
using System;
using JobPortal.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobPortal.API.Migrations
{
    [DbContext(typeof(PortalContext))]
    [Migration("20230606173045_test")]
    partial class test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobPortal.API.Models.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("CompanyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CompanyCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CompanyDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("JobPortal.API.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("JobPortal.API.Models.EmployeeApplyingForJob", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.HasKey("EmployeeId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("EmployeesApplyingForJobs");
                });

            modelBuilder.Entity("JobPortal.API.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobOfferId")
                        .HasColumnType("int");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("JobId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("JobPortal.API.Models.JobOffer", b =>
                {
                    b.Property<int>("JobOfferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobOfferId"));

                    b.Property<string>("Benefits")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("MinimumSalary")
                        .HasColumnType("int");

                    b.Property<int>("NoOfPositions")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("JobOfferId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("JobId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("JobOffer");
                });

            modelBuilder.Entity("JobPortal.API.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("JobPortal.API.Models.EmployeeApplyingForJob", b =>
                {
                    b.HasOne("JobPortal.API.Models.Employee", "Employee")
                        .WithMany("EmployeesApplyingForJobs")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPortal.API.Models.Job", "Job")
                        .WithMany("EmployeesApplyingForJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("JobPortal.API.Models.JobOffer", b =>
                {
                    b.HasOne("JobPortal.API.Models.Company", "Company")
                        .WithMany("JobsOffers")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPortal.API.Models.Job", "Job")
                        .WithOne("JobOffer")
                        .HasForeignKey("JobPortal.API.Models.JobOffer", "JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobPortal.API.Models.User", null)
                        .WithMany("JobOffers")
                        .HasForeignKey("UserId");

                    b.Navigation("Company");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("JobPortal.API.Models.User", b =>
                {
                    b.HasOne("JobPortal.API.Models.Employee", "Employee")
                        .WithOne("User")
                        .HasForeignKey("JobPortal.API.Models.User", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("JobPortal.API.Models.Company", b =>
                {
                    b.Navigation("JobsOffers");
                });

            modelBuilder.Entity("JobPortal.API.Models.Employee", b =>
                {
                    b.Navigation("EmployeesApplyingForJobs");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JobPortal.API.Models.Job", b =>
                {
                    b.Navigation("EmployeesApplyingForJobs");

                    b.Navigation("JobOffer");
                });

            modelBuilder.Entity("JobPortal.API.Models.User", b =>
                {
                    b.Navigation("JobOffers");
                });
#pragma warning restore 612, 618
        }
    }
}
