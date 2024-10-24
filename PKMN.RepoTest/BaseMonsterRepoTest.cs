using PKMN.Models.Monsters;
using PKMN.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKMN.RepoTest
{
    
    public class BaseMonsterRepoTest
    {
        private BattleMonsterRepo _repo;
        public BaseMonsterRepoTest()
        {
            _repo = new BattleMonsterRepo();
        }

        [Fact]
        public void Get_Returns_BaseMonster()
        {
            var repo = new BattleMonsterRepo();

            var item = repo.Get(1);
        }

        [Fact]
        public void Get_Returns_Null()
        {
            var item = _repo.Get(-1);
            Assert.Null(item);
        }
        [Fact]
        public void GetAll_Return_NotNull()
        {
            var items = _repo.GetAll();
            Assert.NotNull(items);
        }

        [Fact]
        public void GetAll_Return_Items()
        {
            var items = _repo.GetAll();
            Assert.NotNull(items);
            Assert.True(items?.Count > 0);   
        }
        [Fact]
        public void Save_Adds_Item_And_Deletes_Removes_It()
        {
            var newPokemon = new BaseMonster(0, "Test Pokemon", Models.MonsterType.Dragon, 100, 50, 50, 45);
            var newId = _repo.Save(newPokemon);
            Assert.True(newId > 0);
            Assert.True(_repo.Delete(newId));
        }
        [Fact]
        public void Save_Update_Item()
        {
            // Get a pokemon from the data source and ensure not null
            var existingPokemon = _repo.GetAll().OrderBy(p => p.Id).First();
            Assert.NotNull(existingPokemon);

            // cache name and create a new, random mame
            var existingName = existingPokemon.Name;
            var newName = Guid.NewGuid().ToString();
            existingPokemon.Name = newName;

            // updating the entry
            var id = _repo.Save(existingPokemon);
            Assert.True(id == existingPokemon.Id);

            //verify if update worked
            var copy = _repo.Get(id);
            Assert.NotNull(copy);
            Assert.True(copy.Name.Equals(newName));
            Assert.True(copy.Attack.Equals(existingPokemon.Attack));
            Assert.False(copy.Name.Equals(existingName));

            // resetting entry to previous fields
            existingName = existingPokemon.Name;
            id = _repo.Save(existingPokemon);
            Assert.True(id.Equals(existingPokemon.Id));
        }
        [Fact]
        public void Save_Disallows_Update_When_False()
        {
            // Get a pokemon from the data source and ensure not null
            var existingPokemon = _repo.GetAll().OrderBy(p => p.Id).First();
            Assert.NotNull(existingPokemon);

            // cache name and create a new, random mame
            var existingName = existingPokemon.Name;
            var newName = Guid.NewGuid().ToString();
            existingPokemon.Name = newName;

            // updating the entry
            var id = _repo.Save(existingPokemon, false);
            Assert.True(id == -1);
        }

        [Fact]
        public void Delete_Returns_False_On_Fail()
        {
            //pokemon id is greater than zero,so -1 _shouldn't_ exist
            Assert.False(_repo.Delete(-1));
        }
    }
}
