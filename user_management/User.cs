namespace Chat_Warriors.user_management;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public int Level {get; set;}
    public int Gold { get; set; }
    public int Energy { get; set; }
    public List<Item> Inventory { get; set; }

    public User(string username)
    {
        Username = username;
        Inventory = new List<Item>();
    }
}
