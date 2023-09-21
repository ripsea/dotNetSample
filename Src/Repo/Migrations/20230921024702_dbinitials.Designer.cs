﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repo.DB;

#nullable disable

namespace Repo.Migrations
{
    [DbContext(typeof(DEVContext))]
    [Migration("20230921024702_dbinitials")]
    partial class dbinitials
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Server.EF.JwtUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("EmailID")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar")
                        .HasColumnName("EmailID");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Password");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("Roles");

                    b.HasKey("ID");

                    b.ToTable("JwtUser");
                });

            modelBuilder.Entity("Server.EF.JwtUserRefreshTokens", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("ExpiredTime")
                        .HasColumnType("datetime")
                        .HasColumnName("ExpiredTime");

                    b.Property<DateTime>("IssuedTime")
                        .HasColumnType("datetime")
                        .HasColumnName("IssuedTime");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar")
                        .HasColumnName("RefreshToken");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar")
                        .HasColumnName("UserName");

                    b.HasKey("ID");

                    b.ToTable("JwtUserRefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
