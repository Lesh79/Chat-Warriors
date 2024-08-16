namespace Chat_Warriors.Game.player_management;

public class EnergySystem
{
    private static readonly int _maxEnergy = 30;
    private static readonly int _regenerateEnergy = 5;

    public static async Task RegenerateEnergy(Player user,GameContext db)
    {
        while (user.Energy < _maxEnergy)
        {   
            await Task.Delay(10000);
            user.Energy += _regenerateEnergy;
            await db.SaveChangesAsync();
        }
        Console.WriteLine("МАКСИМАЛЬНАЯ ЭНЕРГИЯ");
        user.Energy = _maxEnergy;
    }
}