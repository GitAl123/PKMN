using PKMN.Models.Monsters;
using PKMN.Models.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models
{
    public abstract class TurnResult : ITurnResult, IValidatable
    {
        

        public TurnResult()
        {
        }

        public abstract void Apply();
        public virtual bool Validate()
        {
            return true;
        }

        public static TurnResult? Default { get; } = default;
        public BaseMonster Pokemon { get; set; }
        public TurnAction Action { get; set; }
    }

    public class AttackTurnResult : TurnResult
    {
        public BattleMove Attack { get; set; }
        public BaseMonster? Target { get; set; }
        public StatusEffect AppliedStatusCondition { get; set; }
        public int Damage { get; private set; }

        public override void Apply()
        {
            if (Target == null)
                throw new Exception($"Attempted to attack null Pokemon {nameof(Target)}");
            Damage = Convert.ToInt32(CalculateDamage());
            Target.TakeDamage(Damage);
            Target.ApplyStatus(AppliedStatusCondition);

        }
        public double CalculateDamage()
        {
            if (Pokemon == null)
                throw new Exception(nameof(Pokemon) + "is null, can't attack");
            if (Target == null)
                throw new Exception($"Attempted to attack null Pokemon {nameof(Target)}");
            if (Attack == null)
                throw new Exception($"Attempted to operate on a null {nameof(Attack)}");

            var isCrit = Utilities.Calculator.RngIsWithinRange(0.0625);
            return Utilities.Calculator.Round(Utilities.Calculator.CalculateDamage(
               aLevel: Pokemon.Level,
               aPower: Attack.Power,
               aAttack: Pokemon.Attack,
               dDefense: Target.Defense,
               numberOfTargets: 1,
               isCritical: isCrit,
               isSTAB: Pokemon.Type == Attack.Type,
               //when we have non physical attack, this needs to 
               //handle that. Burn only applies to physcial attacks.
               applyBurnModifier: Pokemon.ActiveStatus == StatusEffect.Burn));
        }

    }
    public class SwapTurnResult : TurnResult
    {
        public IList<Monsters.BaseMonster>? Party { get; set; }
        public override void Apply()
        {
            if (Pokemon == null)
                throw new Exception("Attempted to swap with a null target pokemon");
            if (Party == null)
                throw new Exception("Attempted to swap from a null party");
            if (Party.Contains(Pokemon))
                throw new InvalidOperationException("Attempting to swap pokemon that doesn't exist in party");

            Party.Remove(Pokemon);
            Party.Insert(0, Pokemon);
        }
    }
    public interface IValidatable
    {
        bool Validate();
    }
    
    public interface ITurnResult
    {
        void Apply();
    }

    public enum TurnAction
    {
        ApplyDamage,
        SwapPokemon

    }
}
