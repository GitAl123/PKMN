using PKMN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Repo
{
    public interface IRepo<T> where T : class, Iindentifiable
    {
        T? Get(int id);

        IList<T> GetAll();

        /// <summary>
        /// Sava an item by create or update if alloUpdate is true, otherwise
        /// it will return an error if the tiem already exists
        /// </summary>
        /// <param name="item"></param>
        /// <param name="allowUpdate"></param>
        /// <returns></returns>
        int Save(T item, bool allowUpdate = true);

        bool Delete(T item);
        bool Delete(int id);
    }
}
