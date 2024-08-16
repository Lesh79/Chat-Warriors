using System.ComponentModel.DataAnnotations;
using Chat_Warriors.BotService;

namespace Chat_Warriors.Game.player_management;

public class Player
{
    [Key]
    public string UserName { get; set; }
    public long ChatId { get; set; }
    public Condition Status { get; set; } 
    public int Level { get; set; }
    public int Exp { get; set; }
    public int Gold { get; set; }
    public int Energy { get; set; }
    // public List<Item> Inventory { get; set; }

    public Player(string userName, long chatId)
    {
        UserName = userName;
        ChatId = chatId;
        Status = Condition.ReadyToFight;
        Level = 0;
        Gold = 0;
        Energy = 20;
        // Inventory = new List<Item>();
    }
    public async Task ChangeState(Action action, GameContext gameContext)
    {   
        if (action == Action.GoToForest)
        {
            Status = Condition.InForest;
            await gameContext.SaveChangesAsync();
        }
        else if (action == Action.AttackCaravan)
        {
            Status = Condition.AttackCaravan;
            await gameContext.SaveChangesAsync();
            await Task.Delay(6000000);
            Status = Condition.Chill; 
        }
    }

    internal async Task StateToRtf()
    {
        using (var gameContext = new GameContext())
        {
            if (Status != Condition.ReadyToFight)
            {
                await Task.Delay(20000);
                Status = Condition.ReadyToFight;
                gameContext.Players.Update(this);
                await gameContext.SaveChangesAsync();
                await TelegramMessenger.SendMessageAsync(ChatId, "ГОТОВ ПИЗДИТЬСЯ");
            }
        }
    }
    
    public void CheckExp()
    {
        int requiredExp = GetRequiredExpForNextLevel();

        if (Exp >= requiredExp)
            _ = LevelUp();
            
    }

    private int GetRequiredExpForNextLevel()
    {
        return Level == 0 ? 5 : Level * 10 + 10;
    }

    private async Task LevelUp()
    {
        Exp -= GetRequiredExpForNextLevel();
        Level++;
        await TelegramMessenger.SendMessageAsync(ChatId, $"Поздравляем! {UserName} достиг нового уровня {Level}.");
    }
    
}