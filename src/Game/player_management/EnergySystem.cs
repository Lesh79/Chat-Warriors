using Chat_Warriors.BotService;

namespace Chat_Warriors.Game.player_management;

public static class EnergySystem
{
    private const int MaxEnergy = 30;
    private const int RegenerateEnergyAmount = 5;

    private static readonly Dictionary<long, CancellationTokenSource> CancellationTokens = new();

    public static async Task StartRegenerateEnergy(Player player)
    {
        if (CancellationTokens.TryGetValue(player.ChatId, out var existingCts) && !existingCts.IsCancellationRequested)
        {
            return;
        }

        var cts = new CancellationTokenSource();
        CancellationTokens[player.ChatId] = cts;
        var token = cts.Token;

        try
        {
            using var gameContext = new GameContext();
            while (player.Energy < MaxEnergy)
            {
                // Ожидаем задержку или отмену
                await Task.Delay(10000, token);
                
                if (player.Status is not (Condition.ReadyToFight or Condition.Chill))
                {
                    break;
                }

                player.Energy += RegenerateEnergyAmount;
                if (player.Energy > MaxEnergy)
                {
                    player.Energy = MaxEnergy;
                }
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
            }

            if (player.Energy >= MaxEnergy)
            {
                player.Energy = MaxEnergy;
                await TelegramMessenger.SendMessageAsync(player.ChatId, "ЗАРЯЖЕН НА МАКСИМУМ \ud83e\udd73");
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
            }
        }
        catch (TaskCanceledException ts)
        {
            Console.WriteLine($"Ошибка в регенерации энергии {ts}");
        }
    }

    public static void StopRegenerateEnergy(long chatId)
    {
        if (CancellationTokens.TryGetValue(chatId, out var cts))
        {
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
            CancellationTokens.Remove(chatId);
        }
    }
}

