using PKMN.BattleSys;
using PKMN.BattleSys.Exceptions;
using PKMN.Console;
using PKMN.Models;
using PKMN.Models.Monsters;
using PKMN.Models.Players;
using PKMN.Repo;
using PKMN.Utilities;

namespace PKMN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var inputManager = new UserInputManager();
            var displayManager = new DisplayManager();
            var battleManager = new BattleSys.BattleManager();

            var player1 = new HumanPlayer("Ash Ketchup", displayManager, inputManager);
            var player2 = new HumanPlayer("Gary", displayManager, inputManager );

            var moveRepo = new BattleMoveRepo();
            var monsterRepo = new BattleMonsterRepo();

            var tackle = moveRepo.Get(2);
            var growl = moveRepo.Get(3);
            var scratch = moveRepo.Get(4);
            var ember = moveRepo.Get(5);
            var vineWhip = moveRepo.Get(6);

            var bulbasaur = monsterRepo.Get(1);
            bulbasaur?.MoveList.Add(tackle);
            bulbasaur?.MoveList.Add(growl);
            bulbasaur?.MoveList.Add(vineWhip);

            var charmander = monsterRepo.Get(4);
            charmander?.MoveList.Add(scratch);
            charmander?.MoveList.Add(growl);
            charmander?.MoveList?.Add(ember);
            player1.Party = new List<BaseMonster>
            {
                // add Bulbasaur
                bulbasaur, 
                charmander,

            };

            player2.Party = new List<BaseMonster>
            {
                // add charmander
            
                charmander.Clone(),
                bulbasaur.Clone()
            };

            try
            {
                displayManager.DisplayTitle("Pokemon Battle");
                displayManager.DisplayMessage($"Starting Battle between {player1.Name} and {player2.Name}!");

                battleManager.StartBattle(player1, player2, (turnResult) =>
                {
                    // Move this to visiter pattern
                    if (turnResult is AttackTurnResult attackResult)
                    {
                        displayManager.DisplayMessage($" {attackResult?.Pokemon?.Name} inflicted {attackResult?.Damage} damage on {attackResult.Target?.Name} using {attackResult.Attack?.Name}");
                        System.Console.ReadLine();
                    }
                });

                var loser = player1.CurrentPokemon == null ? player1 : player2;
                var winner = player1.CurrentPokemon == null ? player2 : player1;
                displayManager.DisplayMessage($"{loser.Name} is out of Pokemon. {winner.Name} has won the battle! ");
            } 
            catch (BattleStateException e)
            {
                System.Console.WriteLine(" Battle State Exception caught: " + e.Message);
            }

            System.Console.ReadLine();
        }
    }
}
