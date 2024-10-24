using PKMN.BattleSys.Exceptions;
using PKMN.Models;
using PKMN.Models.Players;

namespace PKMN.BattleSys
{
    public class BattleManager
    {
        public void StartBattle(BasePlayer playerOne, BasePlayer playerTwo, Action<ITurnResult?>? onTurnResult = null)
        {
            var playerOneMonster = playerOne.CurrentPokemon;
            var playerTwoMonster = playerTwo.CurrentPokemon;

            if (playerOneMonster == null || playerTwoMonster == null)
            {
                //we cannot battle
                throw new BattleStateException(" A player's party doesn't have valid pokemon ");
            }

            // Check speed of each starting pokemon
            var startingPlayer = playerTwo;
            var otherPlayer = playerOne;
            if (playerOneMonster.Speed >= playerTwoMonster.Speed)
            {
                startingPlayer = playerOne;
                otherPlayer = playerTwo;
            }
            do
            {
               var result = startingPlayer.StartTurn(otherPlayer);

               result?.Apply();
               onTurnResult?.Invoke(result);



                var tmp = startingPlayer;
                startingPlayer = otherPlayer;
                otherPlayer = tmp;
            } while ((startingPlayer.CurrentPokemon != null) && otherPlayer.CurrentPokemon != null);
        }
    }

}
