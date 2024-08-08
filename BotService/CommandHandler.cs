using Chat_Warriors.GameLogic.user_management;
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
            var chatId = message.Chat.Id;

            if (message.Text?.ToLower() == "создать персонажа")
            {
                var newPlayer = new Player(message.Chat.Username);
                await _botClient.SendTextMessageAsync(chatId, $"Герой {newPlayer.Username} создан!");
            }
            else if (message.Text.ToLower() == "лес")
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