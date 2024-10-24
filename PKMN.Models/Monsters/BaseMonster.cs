using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using PKMN.Models.Moves;

namespace PKMN.Models.Monsters
{
    public class BaseMonster : Iindentifiable, IName
    {
        /// <summary>
        /// Pokemon Number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Exp Level
        /// </summary>
        public int Level { get; set; } = 1;
        /// <summary>
        /// Pokemon Type
        /// </summary>
        public MonsterType Type { get; set; }
        /// <summary>
        /// Monster Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Max Monster Health
        /// </summary>
        public int HP { get; set; }

        /// <summary>
        /// Current health of max hp
        /// </summary>
        public int CurrentHP { get; set; }
        /// <summary>
        /// Monster Attack Stat (physical and special)
        /// </summary>
        public int Attack { get; set; }
        /// <summary>
        /// Monster Defense Stat (physical and special)
        /// </summary>
        public int Defense { get; set; }
        /// <summary>
        /// percent chance of landing critical hit, default is 6.25%
        /// </summary>
        public double CritChance { get; set; }

        /// <summary>
        /// Monster Speed Stat used to determine round order
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Up to 4 moves that the monster knows
        /// </summary>
        ///-- TODO: Add controls /restrictions for learning new moves
        ///-- NOTE: When learning a 5th move, it actually needs to replace
        ///-- one of the existing 4 moves
        public virtual IList<Moves.BattleMove> MoveList { get; private set; }
        public StatusEffect ActiveStatus { get; private set; }

        public BaseMonster(int id,string name, MonsterType type, int hP, int attack, int defense, int speed)
        {
            Id = id;
            Type = type;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            HP = hP;
            CurrentHP = hP;
            Attack = attack;
            Defense = defense;
            Speed = speed;

            MoveList = new List<BattleMove> ();

        }

        public string ListMoves()
        {
            if (MoveList == null)
                throw new Exception("Move List is Null, This shouldn't happen ever");

            var sb = new StringBuilder();
            var index = 0;
            foreach (var move in MoveList)
            {
                sb.AppendLine($"{++index}, {move.Name}");
            }
            return sb.ToString();
        }
        public void TakeDamage(int dmg)
        {
            if (dmg > 0)
               CurrentHP -= dmg;
            if (HP <= 0)
            {
                CurrentHP = 0;
                //fainted
                System.Diagnostics.Debug.WriteLine($"{Name} fainted");
            }
        }

        public void ApplyStatus(StatusEffect status)
        {
            if (status == StatusEffect.None)
            {
                System.Diagnostics.Debug.Write($"No status effect to apply.");
            }
            if (ActiveStatus != StatusEffect.None)
            {
                System.Diagnostics.Debug.WriteLine($"{Name} already has an active status: {ActiveStatus}. Cannot apply new status {status}");
                return;
            }
            
            System.Diagnostics.Debug.WriteLine($"{Name} has {status} status now. ");
            ActiveStatus = status;
        }
    }
}
