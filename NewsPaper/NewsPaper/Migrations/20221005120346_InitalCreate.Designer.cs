﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewsPaper.Models;

#nullable disable

namespace NewsPaper.Migrations
{
    [DbContext(typeof(ModelContext))]
    [Migration("20221005120346_InitalCreate")]
    partial class InitalCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("NewsPaper.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WriterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WriterId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("NewsPaper.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("NewsPaper.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("NewsPaper.Models.Article", b =>
                {
                    b.HasOne("NewsPaper.Models.User", "Writer")
                        .WithMany("FavArcticles")
                        .HasForeignKey("WriterId");

                    b.Navigation("Writer");
                });

            modelBuilder.Entity("NewsPaper.Models.Comment", b =>
                {
                    b.HasOne("NewsPaper.Models.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewsPaper.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("Article");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewsPaper.Models.User", b =>
                {
                    b.HasOne("NewsPaper.Models.User", null)
                        .WithMany("FollowedUsers")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("NewsPaper.Models.Article", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("NewsPaper.Models.User", b =>
                {
                    b.Navigation("FavArcticles");

                    b.Navigation("FollowedUsers");
                });
#pragma warning restore 612, 618
        }
    }
}