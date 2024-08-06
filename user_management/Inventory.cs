namespace Chat_Warriors.user_management;

public class Inventory
{
    private readonly User _user;

    public Inventory(User user)
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
