using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models.Players
{
    public class AIPlayer : BasePlayer
    {
        public AIPlayer(string name, IDisplayManager displayManager) : base (name, displayManager)
        {
         
        }

        public override ITurnResult StartTurn(BasePlayer targetPlayer)
        {
            base.StartTurn(targetPlayer);
            throw new NotImplementedException();
        }
    }
}
