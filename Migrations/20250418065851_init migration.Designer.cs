﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetworkingReferenceBasics.Data;

#nullable disable

namespace NetworkingReferenceBasics.Migrations
{
    [DbContext(typeof(NetworkingContext))]
    [Migration("20250418065851_init migration")]
    partial class initmigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetworkingReferenceBasics.Models.AdapterModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DnsSuffix")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InterfaceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDnsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDynamicDnsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("Multicast")
                        .HasColumnType("bit");

                    b.Property<string>("PhysicalAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SuppotingIp4")
                        .HasColumnType("bit");

                    b.Property<bool>("SuppotingIp6")
                        .HasColumnType("bit");

                    b.Property<bool>("isRecieveOnly")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("adapters");
                });
#pragma warning restore 612, 618
        }
    }
}
