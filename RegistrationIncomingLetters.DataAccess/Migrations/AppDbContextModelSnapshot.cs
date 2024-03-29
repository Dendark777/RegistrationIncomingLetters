﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistrationIncomingLetters.DataAccess.Data;

#nullable disable

namespace RegistrationIncomingLetters.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.17")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RegistrationIncomingLetters.DataAccess.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 24,
                            Email = "IvanovII@MyCompany.ru",
                            FirstName = "Иван",
                            LastName = "Иванов",
                            Phone = "8129455315",
                            SecondryName = "Иванович"
                        },
                        new
                        {
                            Id = 2,
                            Age = 63,
                            Email = "PetrovPP@MyCompany.ru",
                            FirstName = "Петр",
                            LastName = "Петров",
                            Phone = "8129455316",
                            SecondryName = "Петрович"
                        });
                });

            modelBuilder.Entity("RegistrationIncomingLetters.DataAccess.Models.Letter", b =>
                {
                    b.Property<decimal>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<decimal>("Id"));

                    b.Property<int>("AddresseeId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddresseeId")
                        .IsUnique();

                    b.HasIndex("SenderId")
                        .IsUnique();

                    b.HasIndex("Id", "SenderId", "AddresseeId");

                    b.ToTable("Letters");
                });

            modelBuilder.Entity("RegistrationIncomingLetters.DataAccess.Models.Letter", b =>
                {
                    b.HasOne("RegistrationIncomingLetters.DataAccess.Models.Employee", "Addressee")
                        .WithOne()
                        .HasForeignKey("RegistrationIncomingLetters.DataAccess.Models.Letter", "AddresseeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("RegistrationIncomingLetters.DataAccess.Models.Employee", "Sender")
                        .WithOne()
                        .HasForeignKey("RegistrationIncomingLetters.DataAccess.Models.Letter", "SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Addressee");

                    b.Navigation("Sender");
                });
#pragma warning restore 612, 618
        }
    }
}
