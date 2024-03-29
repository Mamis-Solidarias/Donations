﻿// <auto-generated />
using System;
using MamisSolidarias.Infrastructure.Donations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MamisSolidarias.WebAPI.Donations.Migrations
{
    [DbContext(typeof(DonationsDbContext))]
    [Migration("20230107132633_CreatedBaseDbModel")]
    partial class CreatedBaseDbModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MamisSolidarias.Infrastructure.Donations.Models.MonetaryDonation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DonatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DonatedAtUTC");

                    b.Property<int?>("DonorId")
                        .HasColumnType("integer");

                    b.Property<string>("Motive")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MonetaryDonations");
                });
#pragma warning restore 612, 618
        }
    }
}
