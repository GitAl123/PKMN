using PKMN.Models.Monsters;
using PKMN.Models.Moves;

namespace PKMN.Repo
{
    public class BattleMonsterRepo : BaseRepo<BaseMonster>
    {
        /// <summary>
        /// this wil set values in order to easier get information fo the main battle console
        /// </summary>
        protected override IList<BaseMonster> Items { get; } = new List<BaseMonster>
        {
            new BaseMonster(1,"Bulbasaur", Models.MonsterType.Grass, 45, 49, 49, 45),
            new BaseMonster(4, "Charmander", Models.MonsterType.Fire, 39, 52, 43, 65),
            new BaseMonster(7, "Squirtle0", Models.MonsterType.Water, 44, 48, 65, 43)
        };
    }
}
