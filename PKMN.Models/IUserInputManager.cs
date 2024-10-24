using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models
{
    public interface IUserInputManager
    {
        int GetUserSelection(string prompt, int mon, int max);
    }
}
