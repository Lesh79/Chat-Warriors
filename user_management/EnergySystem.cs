namespace Chat_Warriors.user_management;

public class EnergySystem
{
    public static void RegenerateEnergy(User user)
    {
        user.Energy = Math.Min(100, user.Energy + 10);
    }
}