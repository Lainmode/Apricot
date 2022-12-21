﻿// <auto-generated />
using System;
using Apricot.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Apricot.Migrations
{
    [DbContext(typeof(ApricotContext))]
    [Migration("20221221180918_edit5")]
    partial class edit5
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Apricot.Database.Chat", b =>
                {
                    b.Property<int>("ChadID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChadID"));

                    b.Property<string>("ChatHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("UserID2")
                        .HasColumnType("int");

                    b.HasKey("ChadID");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Apricot.Database.Contact", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("UserID2")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID2");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Apricot.Database.Space", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Spaces");
                });

            modelBuilder.Entity("Apricot.Database.SpaceUser", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("SpaceID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SpaceID");

                    b.HasIndex("UserID");

                    b.ToTable("SpaceUsers");
                });

            modelBuilder.Entity("Apricot.Database.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Activity")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InActivity")
                        .HasColumnType("bit");

                    b.Property<string>("Nickname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpaceID")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StatusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tag")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("SpaceID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Apricot.Database.Contact", b =>
                {
                    b.HasOne("Apricot.Database.User", "User2")
                        .WithMany("Contacts")
                        .HasForeignKey("UserID2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User2");
                });

            modelBuilder.Entity("Apricot.Database.SpaceUser", b =>
                {
                    b.HasOne("Apricot.Database.Space", "Space")
                        .WithMany()
                        .HasForeignKey("SpaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Apricot.Database.User", "User")
                        .WithMany("SpaceUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Space");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Apricot.Database.User", b =>
                {
                    b.HasOne("Apricot.Database.Space", null)
                        .WithMany("Users")
                        .HasForeignKey("SpaceID");
                });

            modelBuilder.Entity("Apricot.Database.Space", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Apricot.Database.User", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("SpaceUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
