using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models.Moves
{
    public class Struggle : BattleMove
    {
        public Struggle() : base(
          id: 1,
          name: "Struggle",
          type: MonsterType.Normal,
          maxPP: -1,
          description: "default move when pokemon is ot of moves, deals damage to target as well as user",
          power: 10,
          accuracy: 100)
        {
        }

    }
}
