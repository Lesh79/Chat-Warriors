using Chat_Warriors.Game.player_management;
using Microsoft.EntityFrameworkCore;

namespace Chat_Warriors.Game;

public class GameContext: DbContext
{
    public DbSet<Player> Players { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:/Users/temer/RiderProjects/Chat-Warriors/identifier.sqlite");
    }
    
}