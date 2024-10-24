using PKMN.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models
{
    public interface IDisplayManager
    {
        void DisplayMessage(String message);
        void DisplayTitle(String title);
        void DisplayBattleState(BasePlayer activePlayer, BasePlayer targetPlayer);
    }
}
