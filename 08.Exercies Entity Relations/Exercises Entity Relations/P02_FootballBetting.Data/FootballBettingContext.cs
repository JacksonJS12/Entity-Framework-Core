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
    }
}
