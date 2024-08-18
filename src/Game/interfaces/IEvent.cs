using Chat_Warriors.Game.player_management;

namespace Chat_Warriors.Game.interfaces;

public interface IEvent
{
    public static abstract Task GoTo(Player player);

}