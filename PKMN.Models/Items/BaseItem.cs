using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Models.Items
{
    public class BaseItem : Iindentifiable, IName
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public BaseItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
     }
}
