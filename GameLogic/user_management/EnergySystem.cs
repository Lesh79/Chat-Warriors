namespace Chat_Warriors.GameLogic.user_management;

public class EnergySystem
{
    public static void RegenerateEnergy(Player user)
    {
        user.Energy = Math.Min(100, user.Energy + 10);
    }
}