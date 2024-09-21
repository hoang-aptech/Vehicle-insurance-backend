﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vehicle_insurance_backend.DataCtxt;

#nullable disable

namespace vehicle_insurance_backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("vehicle_insurance_backend.models.ClaimDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClaimNumber")
                        .HasColumnType("int");

                    b.Property<int>("ClaimableAmount")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DateOfAccident")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("InsuredAmount")
                        .HasColumnType("int");

                    b.Property<string>("PlaceOfAccident")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PolicyEndDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PolicyNumber")
                        .HasColumnType("int");

                    b.Property<string>("PolicyStartDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ClaimDetails");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.CompanyBillingPolicy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BillNo")
                        .HasColumnType("int");

                    b.Property<string>("CustomerAddProve")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerInformationCustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CustomerPhoneNumber")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PolicyNumber")
                        .HasColumnType("int");

                    b.Property<string>("VehicleBodyNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleEngineNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VehicleRate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerInformationCustomerId");

                    b.ToTable("CompanyBillingPolicy");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.CompanyExpenses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AmountOfExpense")
                        .HasColumnType("int");

                    b.Property<string>("DateOfExpense")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("TypeOfExpense")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CompanyExpenses");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.CustomerInformation", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("CustomerAdd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("CustomerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerInformation");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.Estimate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EstimateNumber")
                        .HasColumnType("int");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehiclePolicyType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VehicleRate")
                        .HasColumnType("int");

                    b.Property<string>("VehicleWarranty")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Estimates");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.InsuranceProcess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CustomerAdd")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerAddProve")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("CustomerInformationCustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("CustomerPhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("CustommerId")
                        .HasColumnType("int");

                    b.Property<string>("PolicyDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PolicyDuration")
                        .HasColumnType("int");

                    b.Property<int>("PolicyNumber")
                        .HasColumnType("int");

                    b.Property<string>("VehicleBodyNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleEngineNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VehicleNumber")
                        .HasColumnType("int");

                    b.Property<int>("VehicleRate")
                        .HasColumnType("int");

                    b.Property<string>("VehicleVersion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleWarranty")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CustomerInformationCustomerId");

                    b.ToTable("InsuranceProcess");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.VehicleInformation", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("VehicleId"));

                    b.Property<string>("VehicleBodyNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleEngineNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehicleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("VehicleNumber")
                        .HasColumnType("int");

                    b.Property<int>("VehicleRate")
                        .HasColumnType("int");

                    b.Property<string>("VehicleVersion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("VehiclesOwnerName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("VehicleId");

                    b.ToTable("VehicleInformation");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.CompanyBillingPolicy", b =>
                {
                    b.HasOne("vehicle_insurance_backend.models.CustomerInformation", "CustomerInformation")
                        .WithMany()
                        .HasForeignKey("CustomerInformationCustomerId");

                    b.Navigation("CustomerInformation");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.Estimate", b =>
                {
                    b.HasOne("vehicle_insurance_backend.models.CustomerInformation", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("vehicle_insurance_backend.models.InsuranceProcess", b =>
                {
                    b.HasOne("vehicle_insurance_backend.models.CustomerInformation", "CustomerInformation")
                        .WithMany()
                        .HasForeignKey("CustomerInformationCustomerId");

                    b.Navigation("CustomerInformation");
                });
#pragma warning restore 612, 618
        }
    }
}