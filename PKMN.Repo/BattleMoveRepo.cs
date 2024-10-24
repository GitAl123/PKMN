using PKMN.Models.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Repo
{
    public class BattleMoveRepo : BaseRepo<BattleMove>
    {
        /// <summary>
        /// This makes it easier to get the inofrmation in order to cleanly set up the battle paramaters 
        /// </summary>
        protected override IList<BattleMove> Items { get; } = new List<BattleMove>
        {
            new Struggle(),
            new BattleMove(2, "Tackle", Models.MonsterType.Normal, 45,"A physical attack in which the user charges, full body, into the foe", 40, 100 ),
            new BattleMove(3, "Growl", Models.MonsterType.Normal, 40, "The user growls in an endearing way, making the foe less wary, The targets attack stat is lowered", 0, 100),
            new BattleMove(4, "Scratch", Models.MonsterType.Normal, 35, "Hard, pointed,sharp claws, rake the target to inflict damage ", 40, 100),
            new BattleMove(5, "Ember", Models.MonsterType.Fire, 25, "The target is attacked with small flames, may leave target with a burn", 40, 100),
            new BattleMove(6, "Vine Whip" , Models.MonsterType.Grass, 25, "The Target is struck with slender whip like vines", 45, 100),
        };
    }
}
