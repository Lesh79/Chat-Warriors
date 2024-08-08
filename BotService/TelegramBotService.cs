using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Chat_Warriors.BotService;

public class TelegramBotService
{
    private readonly ITelegramBotClient _botClient;

    public TelegramBotService(string token)
    {
        _botClient = new TelegramBotClient(token);
    }

    public void Start()
    {
        Console.WriteLine("Запущен бот " + _botClient.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // Receive all update types
        };

        _botClient.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );

        Console.ReadLine();
        cts.Cancel(); // Stop the bot when closing the application
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

        if (update.Type == UpdateType.Message && update.Message?.Text != null)
        {
            var message = update.Message;
            if (message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Соси мои яйца", cancellationToken: cancellationToken);
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!", cancellationToken: cancellationToken);
            }
        }
    }

    private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        return Task.CompletedTask;
    }
}