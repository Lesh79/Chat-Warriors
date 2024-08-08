namespace Chat_Warriors.GameLogic.user_management;

public class Inventory
{
    private Player _user;

    public Inventory(Player user)
    {
        this._user = user;
    }

    public void AddItem(Item item)
    {
        _user.Inventory.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _user.Inventory.Remove(item);
    }

    public List<Item> ListItems()
    {
        return _user.Inventory;
    }
}
