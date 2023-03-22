﻿using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Common;
public class FootballBettingContext : DbContext
{
    //Use it when developing the app
    //When we test the app locally on our PC
    public FootballBettingContext()
    {

    }

    public FootballBettingContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Town> Towns { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<PLayer> PLayers { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<PlayerStatistic> PlayerStatistics { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Bet> Bets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //Set def connection string
            optionsBuilder.UseSqlServer(DbConfig.ConnectionString);
        }

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PlayerStatistic>(entity =>
        {
            entity.HasKey(ps => new { ps.GameId, ps.PlayerId });
        });

        modelBuilder.Entity<Team>(entity => 
        {
            entity
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);

            entity
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
