using Telegram.Bot;

namespace Chat_Warriors.BotService
{
    public static class TelegramMessenger
    {
        private static ITelegramBotClient _botClient = null!;

        public static void Initialize(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public static async Task SendMessageAsync(long chatId, string message)
        {
            if (_botClient == null)
            {
                throw new InvalidOperationException("Telegram bot client is not initialized.");
            }

            await _botClient.SendTextMessageAsync(chatId, message);
        }
    }
}