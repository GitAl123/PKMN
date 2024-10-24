using PKMN.Models;
using PKMN.Models.Moves;
using PKMN.Models.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Console
{
    public class HumanPlayer : BasePlayer
    {
        private IUserInputManager _inputManager;

        public HumanPlayer(string name, IDisplayManager displayManager, IUserInputManager inputManager) : base(name, displayManager)
        {
            _inputManager = inputManager;
        }
        public override ITurnResult? StartTurn(BasePlayer targetPlayer)
        {
            base.StartTurn(targetPlayer);

            ///get current/active pokemon
            if (CurrentPokemon == null)
            {
                DisplayManager.DisplayMessage($" {Name} is out of pokemon, {Name} blacked out.");
                return TurnResult.Default;

            }

            var userChoice = _inputManager.GetUserSelection($" It is {Name}'s turn. What would you like to do?\n" +
               $"\n\t1. Fight" +
               $"\n\t2. Pokemon" +
               $"\n\t3. Bag" +
               $"\n\t4. Run", 1, 4);

            IList<IName> emptyItemList = new List<IName>();

            switch (userChoice)
            {
                case 1: // fight
                    {
                        var choice = MakeSelection(CurrentPokemon.MoveList, $" What do you want {CurrentPokemon.Name} to do?");
                        if (choice <= 0 || choice >= CurrentPokemon.MoveList.Count + 1)
                        {
                            //cancel
                            return StartTurn(targetPlayer);
                        }
                        else
                        {
                            var target = targetPlayer.CurrentPokemon;
                            var battleMove = CurrentPokemon.MoveList[choice - 1];

                            return new AttackTurnResult
                            {
                                Action = TurnAction.ApplyDamage,
                                Attack = battleMove,
                                AppliedStatusCondition = battleMove.StatusAction?.Invoke() ?? StatusEffect.None,
                                Pokemon = CurrentPokemon,
                                Target = target,

                            };
                        }
                    }
                case 2: // Pokemon
                    {
                        var choice = MakeSelection(Party, $" Which pokemon would you like to swap in?");
                        if (choice <= 0 || choice >= Party?.Count + 1)
                        {
                           return StartTurn(targetPlayer);
                        }
                        else
                        {
                            if (Party == null)
                                return TurnResult.Default;
                            var swap = Party[choice - 1];
                            if (swap == CurrentPokemon)
                            {
                                DisplayManager.DisplayMessage($"{CurrentPokemon.Name} is already out");
                                return StartTurn(targetPlayer);
                            }
                            else
                            {
                                DisplayManager.DisplayMessage($"{CurrentPokemon.Name}, come back! Go {swap.Name}");

                                return new SwapTurnResult
                                {
                                    Action = TurnAction.SwapPokemon,
                                    Pokemon = swap,
                                    Party = Party
                                };
                            }
                        }
                    }
                case 3: // Bag
                    MakeSelection(emptyItemList, "Bag is empty");

                    // only option should be cancel
                    break;
                case 4: // Run
                    // if in a wild encounter, try to escape
                    //~~ TODO: Handle Wild Encounter

                    // If in a trainer battle, this isn't allowed
                    // For now, force this second path.
                    MakeSelection(emptyItemList, " Cannot run from a trainer battle");
                    break;
                default: // should be unreachable
                    break;
            }
            return StartTurn(targetPlayer);
        }
        private int MakeSelection<T>(IList<T>? collection, string prompt) where T : IName
        {
            if (collection == null)
            {
                DisplayManager.DisplayMessage("collection is null");
                return -1;
            }

            var sb = new StringBuilder();
            var index = 0;
            foreach (var item in collection)
            {
                sb.AppendLine($"{++index}, {item.Name}");
            }
            sb.AppendLine($"{++index}. Cancel");
            return _inputManager.GetUserSelection($"{prompt} \n{sb}", 1, index);
        }
    }
}
