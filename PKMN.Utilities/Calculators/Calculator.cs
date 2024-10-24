using PKMN.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Utilities
{
    public class Calculator
    {
        public Calculator()
        {

        }
        /// <summary>
        /// calculates if rnadom number is within the rng threshold
        /// </summary>
        /// <param name="chancePercentage">Percentage to check, example, 6.25% will be 6.25 not 0.0625</param>
        /// <returns></returns>
        public static bool RngIsWithinRange(double chancePercentage)
        {
            if ( chancePercentage <= 0)
                return false;
            if ( chancePercentage >= 100d)
                return true;
            return new Random(DateTime.UtcNow.Millisecond).Next() < chancePercentage / 100d;
        }

        public static int Round(double value)
        {
            return (int)(value + 0.5);
        }

        /// <summary>
        /// Calculates the number of individual Hit Points (HP) that the target will lose after all calculations are made
        /// </summary>
        /// <param name="aLevel">Level of attacking pokemon</param>
        /// <param name="aPower">base power of the move</param>
        /// <param name="aAttack">attack power of the pokemon</param>
        /// <param name="dDefense">defense of the pokemon</param>
        /// <param name="numberOfTargets">number of targets move is hitting</param>
        /// <param name="isCritical">chance of criticle hit or increased damage</param>
        /// <param name="isSTAB">Same type attack bonus pokemone gets when move matches pokemon type</param>
        /// <param name="applyBurnModifier">status of burn that halves attack when inflicted</param>
        /// <param name="typeEffectiveness">multiplier used for when pokemon have opposing elemental types or ones they resist</param>
        /// <param name="weatherCondition">conditions of the weather that are currently at play</param>
        /// <param name="other"></param>
        /// <returns>the number of individual Hit Points (HP) that the target will lose after all calculations are made</returns>
        public static double CalculateDamage(
            int aLevel,
            double aPower,
            double aAttack,
            double dDefense,
            int numberOfTargets,
            bool isCritical,
            bool isSTAB,
            bool applyBurnModifier = false ,
            TypeEffectiveness typeEffectiveness = TypeEffectiveness.Neutral,
            Weather weatherCondition = Weather.Normal,
            double other = 1d)
        {
            var typeEffectivenessModifier = 1d;
            switch (typeEffectiveness)
            {
                case TypeEffectiveness.immune:
                    return 0;
                case TypeEffectiveness.DoubleResistant:
                    typeEffectivenessModifier = 0.25;
                    break;
                case TypeEffectiveness.Resistant:
                    typeEffectivenessModifier = 0.5;
                    break;
                case TypeEffectiveness.Weak:
                    typeEffectivenessModifier = 2d;
                    break;
                case TypeEffectiveness.DoubleWeak:
                    typeEffectivenessModifier = 4d;
                    break;
                default: //. neutral (1x modifier)
                    break;
            }

            //Gamerant reference
            var levelModifier = ((2d * aLevel) / 5d) + 2d;
            var moveDamage = ((levelModifier * aPower * (aAttack / dDefense)) / 50d) + 2d;
            // targets (effectiveness) is 1 if single target, else 0.75
            // ignoring battle royale 0.5
            var effectiveness = numberOfTargets > 1 ? 0.75 : 1d;
            var critModifier = isCritical ? 1.5 : 1d;
            var stabModifier = isSTAB ? 1.5 : 1d;
            var burnModifier = applyBurnModifier ? 0.5 : 1d;

            var rng = new Random().Next(85, 100) / 100d;

            return moveDamage * effectiveness * critModifier * stabModifier * burnModifier * rng * other * typeEffectivenessModifier;
        }
    }



    public enum Weather
    {
        Normal,
        Rain,
        Sunlight,
        Hail,
    }
    /// <summary>
    /// Damage Modifier for resistances and weaknesses
    /// </summary>
    public enum TypeEffectiveness
    {
        immune, 
        DoubleResistant,
        Resistant,
        Neutral,
        Weak,
        DoubleWeak,
    }
}
