using Chat_Warriors.BotService;

namespace Chat_Warriors.Game.player_management;

public static class EnergySystem
{
    private const int MaxEnergy = 30;
    private const int RegenerateEnerg = 5;

    public static async Task RegenerateEnergy(Player player)
    {
        using (var gameContext = new GameContext())
        {
            while (player.Energy < MaxEnergy)
            {
                await Task.Delay(300000);
                await TelegramMessenger.SendMessageAsync(player.ChatId, "ПЛЮС");
                player.Energy += RegenerateEnerg;
                gameContext.Players.Update(player);
                await gameContext.SaveChangesAsync();
            }

            await TelegramMessenger.SendMessageAsync(player.ChatId, "ЗАРЯЖЕН НА МАКСИМУМ");
            player.Energy = MaxEnergy;
            gameContext.Players.Update(player);
            await gameContext.SaveChangesAsync();
        }
    }
}