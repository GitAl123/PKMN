using PKMN.Models;
using PKMN.Models.Moves;
using PKMN.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.Repo
{
    public abstract class BaseRepo<T> : IRepo<T> where T : class, Iindentifiable
    {
        /// <summary>
        /// Only children derived from this will be able to set this
        /// </summary>
        protected abstract IList<T> Items { get; }
        public T? Get(int id)
        {
            var item = Items.Where(m => m.Id == id).FirstOrDefault();
            return item.Clone();
        }

        public IList<T> GetAll()
        {
            return Items;
        }

        public int Save(T item, bool allowUpdate = true)
        {
            var existing = Get(item.Id);
            if (item.Id > 0 && existing != null)
            {
                // we have a match, item is existing.
                if (allowUpdate)
                {
                    // update existing item
                    Items[Items.IndexOf(item)] = item;
                    return item.Id;
                }
                return -1;
            }
            //add new item
            item.Id = Items.OrderBy(m => m.Id).Last().Id + 1;
            Items.Add(item);
            return item.Id;
        }
        public bool Delete(T item)
         => Delete(item.Id);

        public bool Delete(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
              return  Items.Remove(item);
            return false;
        }
    }
}
