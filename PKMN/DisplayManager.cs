using PKMN.BattleSys;
using PKMN.Models;
using PKMN.Models.Monsters;
using PKMN.Models.Players;
using PKMN.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Console
{
    public class DisplayManager : IDisplayManager
    {
        public void DisplayTitle(string title)
        {
            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
                
            System.Console.WriteLine("====================================================================\n\t\t\t"
                + title
                + "\n====================================================================");
            System.Console.ForegroundColor = currentColor;
        }
        public void DisplayMessage(string message)
        {
            System.Console.WriteLine(message);
        }

        public void clear()
        {
            System.Console.Clear();
        }

        BasePlayer _bottomPlayerRef;
        BasePlayer _topPlayerRef;
        public void DisplayBattleState(BasePlayer  activePlayer , BasePlayer targetPlayer)
        {
            clear();

          if (_bottomPlayerRef == null || _topPlayerRef == null)
            {

                if (activePlayer is HumanPlayer hPlayer)
                {
                    _bottomPlayerRef = activePlayer;
                    _topPlayerRef = targetPlayer;
                }
                else
                {
                    _bottomPlayerRef = targetPlayer;
                    _topPlayerRef = activePlayer;
                }
            }

            //========
            // Game Title
            //========
            // >>  Player
            //      Pokemon (lv.1)
            //      [========  ] 13/26
            //                               Ai Player 2
            //                             Pokemon (lv.1)
            //                             [========= ] 25/30
            //-----------------------------------------------------
            DisplayTitle("Pokeman Battle");

            var currentColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = ConsoleColor.Green;
            var display = string.Format(
                "{0,65}    \n" +
                "{1,65}\n" +
                "{2,65}\n" +


                "    {3}    \n" +
                "    {4}\n" +
                "    {5}\n" +
                "====================================================================\n",
                 _topPlayerRef.Name,
                $" {_topPlayerRef.CurrentPokemon?.Name} (Lv{_topPlayerRef.CurrentPokemon?.Level})",
                GetHealthBar(_topPlayerRef.CurrentPokemon),
                 _bottomPlayerRef.Name,
                $"{_bottomPlayerRef.CurrentPokemon?.Name} (Lv{_bottomPlayerRef.CurrentPokemon?.Level})",
                GetHealthBar(_bottomPlayerRef.CurrentPokemon));

            DisplayMessage(display.Replace($"    {activePlayer.Name}    ",$" >> {activePlayer.Name} <<" ));

            System.Console.ForegroundColor = currentColor;
        }   
        
        private string GetHealthBar(BaseMonster monster)
        {
            if (monster == null)
                return "[         ] --/--";
            // [=========] 25/30
            var hpPercent = (double)monster.CurrentHP / (double)monster.HP;
            var segments = (int)(10 * hpPercent);
            var sb = new StringBuilder("[");
            for(int i = 0; i < segments; i++)
            {
                sb.Append("=");
            }
            for (int j = segments; j < 10; j++)
            {
                sb.Append(" ");
            }
            sb.Append($"] {monster.CurrentHP}/ {monster.HP}");
            return sb.ToString();
        }
    }
}
