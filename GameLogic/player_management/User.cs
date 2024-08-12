using Action = Chat_Warriors.GameLogic.player_management.Action;

namespace Chat_Warriors.GameLogic.player_management;

public class Player
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public Condition Status { get; set; } 
    public int Level { get; set; }
    public int Exp { get; set; }
    public int Gold { get; set; }
    public int Energy { get; set; }
    // public List<Item> Inventory { get; set; }

    public Player(string username)
    {
        Username = username;
        Status = Condition.ReadyToFight;
        Level = 0;
        Gold = 0;
        Energy = 20;
        // Inventory = new List<Item>();
    }
    public async Task ChangeState(Action action)
    {
        if (action == Action.GoToForest)
        {
            Status = Condition.InForest;
            // await Task.Delay(10800000); // 3 hours
            await Task.Delay(10000);
            Status = Condition.Chill;
        }
        else if (action == Action.AttackCaravan)
        {
            Status = Condition.AttackCaravan;
            await Task.Delay(6000000);
            Status = Condition.Chill;
        }
    }

    public void CheckExp()
    {
        int requiredExp = GetRequiredExpForNextLevel();

        if (Exp >= requiredExp)
            LevelUp();
            
    }

    private int GetRequiredExpForNextLevel()
    {
        if (Level == 0)
            return 5;
        else
            return Level * 10 + 10;
    }

    private void LevelUp()
    {
        Exp -= GetRequiredExpForNextLevel();
        Level++;
        Console.WriteLine($"Поздравляем! Выдостигли нового уровня {Level}.");
    }
    
}