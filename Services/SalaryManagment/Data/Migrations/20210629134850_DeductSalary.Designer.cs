﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SalaryManagment.Data;

namespace SalaryManagment.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210629134850_DeductSalary")]
    partial class DeductSalary
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("SalaryManagment.Entities.Appraisal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Percent")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Appraisals");
                });

            modelBuilder.Entity("SalaryManagment.Entities.Bonus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Bonuss");
                });

            modelBuilder.Entity("SalaryManagment.Entities.Restrictions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AllowedHoliday")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AllowedLates")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HolidayDeduction")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Restrictionss");
                });

            modelBuilder.Entity("SalaryManagment.Entities.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AppraisalId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SalaryBreakdownId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SalaryHistoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AppraisalId");

                    b.HasIndex("SalaryBreakdownId");

                    b.HasIndex("SalaryHistoryId");

                    b.ToTable("Salarys");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryBreakdown", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("DaySalary")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SalaryBreakdowns");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryByMonth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Deduction")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Month")
                        .HasColumnType("TEXT");

                    b.Property<int>("NetAmount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("SalaryHistoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SalaryHistoryId");

                    b.ToTable("SalaryByMonth");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryDeduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reason")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SalaryDeductions");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SalaryHistorys");
                });

            modelBuilder.Entity("SalaryManagment.Entities.Salary", b =>
                {
                    b.HasOne("SalaryManagment.Entities.Appraisal", "Appraisal")
                        .WithMany()
                        .HasForeignKey("AppraisalId");

                    b.HasOne("SalaryManagment.Entities.SalaryBreakdown", "SalaryBreakdown")
                        .WithMany()
                        .HasForeignKey("SalaryBreakdownId");

                    b.HasOne("SalaryManagment.Entities.SalaryHistory", "SalaryHistory")
                        .WithMany()
                        .HasForeignKey("SalaryHistoryId");

                    b.Navigation("Appraisal");

                    b.Navigation("SalaryBreakdown");

                    b.Navigation("SalaryHistory");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryByMonth", b =>
                {
                    b.HasOne("SalaryManagment.Entities.SalaryHistory", null)
                        .WithMany("SalaryByMonth")
                        .HasForeignKey("SalaryHistoryId");
                });

            modelBuilder.Entity("SalaryManagment.Entities.SalaryHistory", b =>
                {
                    b.Navigation("SalaryByMonth");
                });
#pragma warning restore 612, 618
        }
    }
}
