﻿// <auto-generated />
using System;
using BettingTracker.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BettingTracker.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BettingTracker.Server.Entities.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Italy",
                            Name = "Serie A"
                        },
                        new
                        {
                            Id = 2,
                            Country = "England",
                            Name = "Premier League"
                        },
                        new
                        {
                            Id = 3,
                            Country = "Spain",
                            Name = "La Liga"
                        });
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("KickOff")
                        .HasColumnType("datetime2");

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Odds")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PredictionTypeId")
                        .HasColumnType("int");

                    b.Property<decimal>("Profit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Stake")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeamToWin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.HasIndex("PredictionTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Predictions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AwayTeam = "Napoli",
                            HomeTeam = "Spezia",
                            KickOff = new DateTime(2023, 3, 23, 15, 15, 37, 388, DateTimeKind.Local).AddTicks(9792),
                            LeagueId = 1,
                            Odds = "1.40",
                            Profit = 120m,
                            Stake = "300",
                            Status = "Won",
                            TeamToWin = "Napoli",
                            Tip = "2"
                        },
                        new
                        {
                            Id = 2,
                            AwayTeam = "Manchester City",
                            HomeTeam = "Tottenham",
                            KickOff = new DateTime(2023, 3, 23, 15, 15, 37, 388, DateTimeKind.Local).AddTicks(9799),
                            LeagueId = 2,
                            Odds = "2.40",
                            Profit = -300m,
                            Stake = "300",
                            Status = "Lost",
                            TeamToWin = "Manchester City",
                            Tip = "2"
                        });
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.PredictionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PredictionTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Live"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Pre Game"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Over 1.5 Goals"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Over 2.5 Goals"
                        });
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LeagueId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LeagueId = 3,
                            Name = "Real Madrid"
                        },
                        new
                        {
                            Id = 2,
                            LeagueId = 3,
                            Name = "Barcelona"
                        },
                        new
                        {
                            Id = 3,
                            LeagueId = 2,
                            Name = "Manchester City"
                        },
                        new
                        {
                            Id = 4,
                            LeagueId = 2,
                            Name = "Liverpool"
                        });
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.Prediction", b =>
                {
                    b.HasOne("BettingTracker.Server.Entities.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BettingTracker.Server.Entities.PredictionType", null)
                        .WithMany("Predictions")
                        .HasForeignKey("PredictionTypeId");

                    b.HasOne("BettingTracker.Server.Entities.User", null)
                        .WithMany("Predictions")
                        .HasForeignKey("UserId");

                    b.Navigation("League");
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.Team", b =>
                {
                    b.HasOne("BettingTracker.Server.Entities.League", "League")
                        .WithMany()
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.PredictionType", b =>
                {
                    b.Navigation("Predictions");
                });

            modelBuilder.Entity("BettingTracker.Server.Entities.User", b =>
                {
                    b.Navigation("Predictions");
                });
#pragma warning restore 612, 618
        }
    }
}
