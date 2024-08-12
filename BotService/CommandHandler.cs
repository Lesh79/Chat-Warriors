using Chat_Warriors.GameLogic.player_management;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Chat_Warriors.BotService
{
    public class CommandHandler
    {
        private readonly ITelegramBotClient _botClient;

        public CommandHandler(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }


        public async Task HandleCommandAsync(Message message)
        {
            using (var db = new GameContext())
            {
                var chatId = message.Chat.Id;
                if (message.Text?.ToLower() == "создать персонажа")
                {
                    var existingPlayer = db.Players.SingleOrDefault(p => p.Username == message.Chat.Username);
                    if (existingPlayer == null)
                    {
                        var newPlayer = new Player(message.Chat.Username!);
                        await db.Players.AddAsync(newPlayer);
                        await db.SaveChangesAsync();
                        var addedPlayer = db.Players.SingleOrDefault(p => p.Username == message.Chat.Username);
                        if (addedPlayer != null)
                        {
                            await _botClient.SendTextMessageAsync(chatId, $"Герой {newPlayer.Username} создан!");
                        }
                        else
                        {
                            await _botClient.SendTextMessageAsync(chatId, "Ошибка при создании персонажа!");
                        }
                    }
                    else await _botClient.SendTextMessageAsync(chatId, "Персонаж уже существует");
                }
                else if (message.Text?.ToLower() == "Лес")
                {
                    // var gameLogic = new GameLogic.GameLogic(player);
                    //
                    // await gameLogic.GoToForest();
                    //
                    // await _botClient.SendTextMessageAsync(chatId, $"Вы пошли в лес! Текущий статус: {player.Status}. Ваше золото: {player.Gold}");
                }
            }
        }
    }
}