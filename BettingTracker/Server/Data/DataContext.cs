using BettingTracker.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace BettingTracker.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.Entity<Team>()
       //.HasOne(t => t.League)
       //.WithMany()
       //.HasForeignKey(t => t.LeagueId);



        //modelBuilder.Entity<Prediction>()
        //    .HasOne(p => p.HomeTeam)
        //    .WithMany()
        //    .HasForeignKey(p => p.HomeTeamId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<Prediction>()
        //    .HasOne(p => p.AwayTeam)
        //    .WithMany()
        //    .HasForeignKey(p => p.AwayTeamId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //modelBuilder.Entity<User>()
        //.HasMany(u => u.Predictions)
        //.WithOne(p => p.User)
        //.HasForeignKey(p => p.UserId);

        //modelBuilder.Entity<Prediction>()
        //    .HasOne(p => p.PredictionType)
        //    .WithMany(pt => pt.Predictions)
        //    .HasForeignKey(p => p.PredictionTypeId);

        modelBuilder.Entity<League>().HasData(
            new League { Id = 1, Name = "Serie A", Country = "Italy" },
            new League { Id = 2, Name = "Premier League", Country = "England" },
            new League { Id = 3, Name = "La Liga", Country = "Spain" }
            );

        modelBuilder.Entity<Prediction>().HasData(
                new Prediction
                {
                    Id = 1,
                    KickOff = DateTime.Now,
                    LeagueId = 1,
                    HomeTeam = "Spezia",
                    AwayTeam = "Napoli",
                    Tip = "2",
                    TeamToWin = "Napoli",
                    Odds = "1.40",
                    Stake = "300",
                    Profit = 120,
                    Status = "Won"
                },
                new Prediction
                {
                    Id = 2,
                    KickOff = DateTime.Now,
                    LeagueId = 2,
                    HomeTeam = "Tottenham",
                    AwayTeam = "Manchester City",
                    Tip = "2",
                    TeamToWin = "Manchester City",
                    Odds = "2.40",
                    Stake = "300",
                    Profit = -300,
                    Status = "Lost"
                }
                );

        modelBuilder.Entity<Team>().HasData(
             new Team
             {
                 Id = 1,
                 Name = "Real Madrid",
                 LeagueId = 3,
             },
             new Team
             {
                 Id = 2,
                 Name = "Barcelona",
                 LeagueId = 3,
             },
             new Team
             {
                 Id = 3,
                 Name = "Manchester City",
                 LeagueId = 2,
             },
             new Team
             {
                 Id = 4,
                 Name = "Liverpool",
                 LeagueId = 2,
             }
             );

        modelBuilder.Entity<PredictionType>().HasData(
            new PredictionType { Id = 1, Name = "Live" },
            new PredictionType { Id = 2, Name = "Pre Game" },
            new PredictionType { Id = 3, Name = "Over 1.5 Goals"},
            new PredictionType { Id = 4, Name = "Over 2.5 Goals"}
            );
    }

    public DbSet<User> Users { get; set; }
    public DbSet<League> Leagues { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<PredictionType> PredictionTypes { get; set; }


}
