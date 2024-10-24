using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models.Moves
{
    public class BattleMove : Iindentifiable, IName
    {
        /// <summary>
        /// Pokemon Number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Type related to attack
        /// </summary>
        public MonsterType Type { get; set; }
        /// <summary>
        /// Name of attack
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Power Points of move/Total amounts of move usage
        /// </summary>
        public int MaxPP { get; set; }
        /// <summary>
        /// Current Amount of Power Points
        /// </summary>
        public int CurrentPP { get; set; }
        /// <summary>
        /// Description of move and what it does
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Current Attack strength of move not related to 
        /// pokemon atk stat
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// Accuracy of the battle move hitting the target
        /// </summary>
        public int Accuracy { get; set; }
        public Func<StatusEffect>? StatusAction { get; private set; }

        public BattleMove(int id, string name, MonsterType type, int maxPP, string description, int power, int accuracy)
        {
            Id = id;
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MaxPP = maxPP;
            CurrentPP = maxPP;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Power = power;
            Accuracy = accuracy;
        }
    }
}
