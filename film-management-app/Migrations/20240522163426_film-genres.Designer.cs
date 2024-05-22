﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using film_management_app.Server;

#nullable disable

namespace film_management_app.Server.Migrations
{
    [DbContext(typeof(FilmManagementDbContext))]
    [Migration("20240522163426_film-genres")]
    partial class filmgenres
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.30");

            modelBuilder.Entity("film_management_app.Server.FeeNegotiation", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("NewFee")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("OldFee")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("FeeNegotiation");
                });

            modelBuilder.Entity("film_management_app.Server.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Budget")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasBeenFilmed")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PlannedShootingEndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("PlannedShootingStartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagLine")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("film_management_app.Server.FilmDirector", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FilmId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "FilmId");

                    b.HasIndex("FilmId")
                        .IsUnique();

                    b.ToTable("FilmDirectors");
                });

            modelBuilder.Entity("film_management_app.Server.FilmStar", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FilmId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("AcceptedRole")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Fee")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "FilmId");

                    b.HasIndex("FilmId");

                    b.ToTable("FilmStars");
                });

            modelBuilder.Entity("film_management_app.Server.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("film_management_app.Server.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasEverLoggedIn")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsActor")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDirector")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.Property<int>("FilmsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("GenresId")
                        .HasColumnType("INTEGER");

                    b.HasKey("FilmsId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("FilmGenre");
                });

            modelBuilder.Entity("film_management_app.Server.FeeNegotiation", b =>
                {
                    b.HasOne("film_management_app.Server.Film", null)
                        .WithMany("FeeNegotiations")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("film_management_app.Server.FilmDirector", b =>
                {
                    b.HasOne("film_management_app.Server.Film", "Film")
                        .WithOne("Director")
                        .HasForeignKey("film_management_app.Server.FilmDirector", "FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("film_management_app.Server.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("film_management_app.Server.FilmStar", b =>
                {
                    b.HasOne("film_management_app.Server.Film", "Film")
                        .WithMany("Actors")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("film_management_app.Server.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Film");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FilmGenre", b =>
                {
                    b.HasOne("film_management_app.Server.Film", null)
                        .WithMany()
                        .HasForeignKey("FilmsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("film_management_app.Server.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("film_management_app.Server.Film", b =>
                {
                    b.Navigation("Actors");

                    b.Navigation("Director")
                        .IsRequired();

                    b.Navigation("FeeNegotiations");
                });
#pragma warning restore 612, 618
        }
    }
}
