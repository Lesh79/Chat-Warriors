using Chat_Warriors.BotService;
using Chat_Warriors.Game.player_management;

namespace Chat_Warriors.Game;

public static class Forest
{
    public static async Task GoToForest(Player player)
    {
        await TelegramMessenger.SendMessageAsync(player.ChatId, $"Герой {player.UserName} ушёл в лес!");
        if (player is { Energy: >= 10, Status: Condition.ReadyToFight })
        {
            player.Energy -= 10;
            player.Gold += 10;
            player.Exp += 5;
            _ = GoToForestAsync(player);
            player.CheckExp();
            //TODO: random items 
        }
        else
        {
            await TelegramMessenger.SendMessageAsync(player.ChatId, 
                "Недостаточно энергии или игрок не готов к бою!");
        }
    }

    private static async Task GoToForestAsync(Player player)
    {
        try
        {
            using (var gameContext = new GameContext())
            {
                player.Status = Condition.InForest;
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine("Состояние игрока сохранено: InForest");

                await Task.Delay(10000);

                player.Status = Condition.Chill;
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine("Состояние игрока сохранено: Chill");

                await TelegramMessenger.SendMessageAsync(player.ChatId, 
                    $"Герой {player.UserName} вернулся из леса!");

                await player.StateToRtf();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
    
}