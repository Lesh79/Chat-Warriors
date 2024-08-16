using Chat_Warriors.Game;
using Chat_Warriors.Game.player_management;
using Telegram.Bot.Types;

namespace Chat_Warriors.BotService
{
    public static class CommandHandler
    {
        public static async Task HandleCommandAsync(Message message)
        {
            await using var db = new GameContext();
            var chatId = message.Chat.Id;
            var currentPlayer = db.Players.SingleOrDefault(p => p.UserName == message.Chat.Username);
            switch (message.Text)
            {
                case "/start":
                {
                    await TelegramMessenger.SendMessageAsync(chatId,
                        $"Доступные команды:\n- /create\n- /hero\n- /forest\n- /attack");
                    break;
                }
                case "/hero":
                {
                    if (currentPlayer == null)
                        await TelegramMessenger.SendMessageAsync(chatId, "Создай персонажа!");
                    else
                    {
                        await TelegramMessenger.SendMessageAsync(chatId,
                            $"\ud83c\udfb0 Name: \"{currentPlayer.UserName}\" " +
                            $"\n\ud83c\udf15 Status: {currentPlayer.Status}" +
                            $"\n\ud83d\udd95 Level:{currentPlayer.Level} \n\ud83e\udd47 Gold: " +
                            $"{currentPlayer.Gold} \n\u26a1\ufe0f Energy: {currentPlayer.Energy}");
                    }

                    break;
                }
                // Hero Initialization
                case "/create":
                {
                    if (currentPlayer == null)
                    {
                        var newPlayer = new Player(message.Chat.Username!, chatId);
                        await db.Players.AddAsync(newPlayer);
                        await db.SaveChangesAsync();
                        var addedPlayer = db.Players.SingleOrDefault(p => p.UserName == message.Chat.Username);
                        if (addedPlayer != null)
                        {
                            await TelegramMessenger.SendMessageAsync(chatId,
                                $"Герой {newPlayer.UserName} создан!");
                            _ = Task.Run(() => EnergySystem.RegenerateEnergy(newPlayer));

                        }
                        else
                        {
                            await TelegramMessenger.SendMessageAsync(chatId, "Ошибка при создании персонажа!");
                        }
                    }
                    else await TelegramMessenger.SendMessageAsync(chatId, "Персонаж уже существует");

                    break;
                }
                // The Hero's Travels
                case "/forest":
                {
                    if (currentPlayer != null)
                    {
                        try
                        {
                            await Forest.GoToForest(currentPlayer);
                            await db.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка сохранения в базу данных: {ex.Message}");
                            await TelegramMessenger.SendMessageAsync(chatId, 
                                "Произошла ошибка при сохранении данных.");
                        }
                    }
                    else
                    {
                        await TelegramMessenger.SendMessageAsync(chatId,
                            $"Персонаж не найден! Создайте персонажа командой 'создать персонажа'");
                    }

                    break;
                }
                case "/attack":
                {
                    if (currentPlayer != null) db.Players.Remove(currentPlayer);
                    await db.SaveChangesAsync();
                    await TelegramMessenger.SendMessageAsync(chatId,
                        $"Пока что эта залупа будет удалять перса, так надо мне");
                    break;
                }
                default:
                    await TelegramMessenger.SendMessageAsync(chatId, "ИДИ НАХУЙ ПОКА НИЧЕГО НЕТ");
                    break;
            }
        }
    }
}