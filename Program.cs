using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace Chat_Warriors;

class Program
{   
    static ITelegramBotClient bot = new TelegramBotClient("7317444461:AAGusG-wmkd2eVh9G8q_hWV4DhbmcOInTQQ");

    private static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message?.Text?.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Соси мои яйца");
                return;
            }
            await botClient.SendTextMessageAsync(message?.Chat ?? throw new InvalidOperationException(), "Привет-привет!!");
        }
    }

    private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        return Task.CompletedTask;
    }
    
    static void Main(string[] args)
    {
        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);
        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
    }
}