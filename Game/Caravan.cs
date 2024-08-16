using Chat_Warriors.Game.player_management;
using Action = Chat_Warriors.Game.player_management.Action;

namespace Chat_Warriors.Game;

public static class Caravan
{
    public static async Task AttackCaravan(Player player, GameContext gameContext)
    {
        if (player.Energy >= 30 && player.Status == Condition.ReadyToFight)
        {
            player.Energy -= 30;
            await player.ChangeState(Action.AttackCaravan, gameContext);
            player.Exp += 15;
            player.CheckExp();
            //TODO: some logic
        }
    }
}