using Microsoft.EntityFrameworkCore;

namespace Chat_Warriors.GameLogic.player_management;
[Keyless]
public class Item
{
    public string Name { get; set; }
    public int Value { get; set; }
    public int PlayerId { get; set; }
    public Player? Player { get; set; }
    
}