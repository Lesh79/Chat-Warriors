using Chat_Warriors.BotService;
using Chat_Warriors.Game.interfaces;
using Chat_Warriors.Game.player_management;

namespace Chat_Warriors.Game;

public class Forest : IEvent
{
    public static async Task GoTo(Player player)
    {
        switch (player)
        {
            case { Energy: >= 10, Status: Condition.ReadyToFight }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, $"Герой {player.UserName} ушёл в лес!\ud83c\udf33");
                player.Energy -= 10;
                _ = GoToForestAsync(player);
                player.Gold += 10;
                player.Exp += 5;
                player.CheckExp();
                // TODO: random items 
                break;
            case { Status: Condition.InForest }:
                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Герой {player.UserName} уже находится в лесу\ud83c\udf32");
                break;
            case { Status: Condition.Chill }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, 
                    $"\ud83d\ude34{player.UserName} отдыхает!");
                break;
            case { Energy: < 10 }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, $"\u26a1\ufe0fНедостаточно энергии!");
                break;
            default:
                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Неизвестная ошибка! Напишите разработчику @Lesh77\ud83d\ude91");
                break;
        }
    }

    private static async Task GoToForestAsync(Player player)
    {
        try
        {
            using (var gameContext = new GameContext())
            {
                player.Status = Condition.InForest;
                EnergySystem.StopRegenerateEnergy(player.ChatId);
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine($"Состояние игрока {player.UserName} сохранено: InForest");

                await Task.Delay(10000);

                player.Status = Condition.Chill;
                _ = Task.Run(() => EnergySystem.StartRegenerateEnergy(player));
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine($"Состояние игрока {player.UserName} сохранено: Chill");

                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Герой {player.UserName} вернулся из леса!\ud83e\udd20");

                await player.StateToRtf();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}