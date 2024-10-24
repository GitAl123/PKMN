using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKMN.Models.Monsters;

namespace PKMN.Models.Players
{
    public abstract class BasePlayer
    {
        protected IDisplayManager DisplayManager;
        public string Name { get; set; }
        public BasePlayer(string name, IDisplayManager displayManager)
        {
            Name = name;
            DisplayManager = displayManager;
        }
        /// <summary>
        /// Current Party of Pokemon, Max of 6 and needs controls/restrcitions 
        /// for swapping new pokemon with existing
        /// </summary>
        public virtual IList<Monsters.BaseMonster> Party { get; set; }

        public Monsters.BaseMonster? CurrentPokemon => Party?.Where(p => p.CurrentHP > 0)?.FirstOrDefault();

        public virtual string ListParty()
        {
            if (Party == null)
                return $"{Name} is out of Pokemon";

            var sb = new StringBuilder();
            foreach (var monster in Party)
            {
                sb.AppendLine($" {monster.Name} (Lv. {monster.Level})");
            }
            return sb.ToString();
        }
        public virtual ITurnResult? StartTurn(BasePlayer targetPlayer)
        {
            DisplayManager.DisplayBattleState(this, targetPlayer);
            return TurnResult.Default;
        }
    }
}
