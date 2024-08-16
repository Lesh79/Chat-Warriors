using Chat_Warriors.BotService;

namespace Chat_Warriors;

class Program
{
    static void Main(string[] args)
    {
        var botService = new TelegramBotService("7317444461:AAGusG-wmkd2eVh9G8q_hWV4DhbmcOInTQQ");
        botService.Start();
    }
    // static async Task Main(string[] args)
    // {
    //     var user = new Player("Huesos");
    //     var gm = new Game.GameLogic(user);
    //      await gm.GoToForest();
    // }
}