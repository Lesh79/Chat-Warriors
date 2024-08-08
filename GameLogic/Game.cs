using Chat_Warriors.GameLogic.user_management;
using Action = Chat_Warriors.GameLogic.user_management.Action;

namespace Chat_Warriors.GameLogic;

public class GameLogic
{
    private Player _user;

    public GameLogic(Player user)
    {
        this._user = user;
    }

    public async Task GoToForest()
    {
        if (_user.Energy >= 10 & _user.Status == Condition.ReadyToFight )
        {
            _user.Energy -= 10;
            await _user.ChangeState(Action.GoToForest);
            _user.Gold += 10;
            _user.Exp += 5;
            // TODO: random items 
        }
        else
        {
            //TODO: print in tg chat about mistake
        }
    }

    public async Task AttackCaravan()
    {
        if (_user.Energy >= 30 & _user.Status == Condition.ReadyToFight)
        {
            _user.Energy -= 30;
            await _user.ChangeState(Action.AttackCaravan);
            _user.Exp += 15;
            //TODO: some logic
        }
    }
}