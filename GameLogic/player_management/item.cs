using Microsoft.EntityFrameworkCore;

namespace Chat_Warriors.GameLogic.player_management;
[Keyless]
public class Item
{
    public string Name { get; set; }
    public int Price{ get; set; }
}