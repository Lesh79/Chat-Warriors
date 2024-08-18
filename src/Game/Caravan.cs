using Chat_Warriors.BotService;
using Chat_Warriors.Game.interfaces;
using Chat_Warriors.Game.player_management;

namespace Chat_Warriors.Game;

public class Caravan : IEvent
{
    public static async Task GoTo(Player player)
    {
        switch (player)
        {
            case { Energy: >= 30, Status: Condition.ReadyToFight }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, 
                    $"Герой {player.UserName} убежал вырезать циган!\ud83e\udee1");
                player.Energy -= 30;
                player.Exp += 15;
                _ = GoToAttackCaravan(player);
                player.CheckExp();
                break;
            case { Status: Condition.AttackCaravan }:
                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Герой {player.UserName} уже на резне!\ud83d\udc80");
                break;
            case { Status: Condition.Chill }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, 
                    $"\ud83d\ude34{player.UserName} отдыхает!");
                break;
            case { Energy: < 30 }:
                await TelegramMessenger.SendMessageAsync(player.ChatId, $"\u26a1\ufe0fНедостаточно энергии!");
                break;
            default:
                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Неизвестная ошибка! Напишите разработчику @Lesh77\ud83d\ude91");
                break;
        }
    }

    private static async Task GoToAttackCaravan(Player player)
    {
        try
        {
            using (var gameContext = new GameContext())
            {
                player.Status = Condition.AttackCaravan;
                EnergySystem.StopRegenerateEnergy(player.ChatId);
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine($"Состояние игрока {player.UserName} сохранено: AttackCaravan\ud83d\ude43");

                await Task.Delay(1000000);

                player.Status = Condition.Chill;
                _ = Task.Run(() => EnergySystem.StartRegenerateEnergy(player));
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
                Console.WriteLine($"Состояние игрока {player.UserName} сохранено: Chill");

                await TelegramMessenger.SendMessageAsync(player.ChatId,
                    $"Герой {player.UserName} вернулся!");

                await player.StateToRtf();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}