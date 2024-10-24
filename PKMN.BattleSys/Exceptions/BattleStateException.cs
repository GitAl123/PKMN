using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.BattleSys.Exceptions
{
    public class BattleStateException : Exception
    {
        public BattleStateException(string? message) : base(message) { } 
    }
}
