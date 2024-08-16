using Microsoft.EntityFrameworkCore;

namespace Chat_Warriors.GameLogic.player_management;
[Keyless]
public class Item
{
    public Item(string name, int price){ //конструктор айтема
        Name = name;
        Price = price;
    }

    public string Name { get; set; }
    public int Price{ get; set; }
}