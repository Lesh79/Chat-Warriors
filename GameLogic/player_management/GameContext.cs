using Microsoft.EntityFrameworkCore;

namespace Chat_Warriors.GameLogic.player_management;

public class GameContext: DbContext
{
    public DbSet<Player> Players { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:/Users/temer/RiderProjects/Chat-Warriors/identifier.sqlite");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()

            .HasKey(p => p.UserId);
    }
}