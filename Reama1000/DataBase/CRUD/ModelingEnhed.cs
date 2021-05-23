using DataBase.Models;
using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataBase.CRUD
{
    public class ModelingEnhed : IDataBase<Enhed, int>
    {
        Reama1000Context _Context { get; set; }

        public ModelingEnhed(Reama1000Context context)
        {
            _Context = context;
        }


        public async Task<List<Enhed>> ReadAllAsync()
        {
            return _Context.Enheds.ToList();
        }
        public async Task<List<Enhed>> ReadAsync(Func<Enhed, bool> search)
        {
            return _Context.Where(search).ToList(); ;
        }
        public async Task CreateAsync(Enhed obj)
        {
            await _Context.Enheds.AddAsync(obj);
            await _Context.SaveChangesAsync();
        }

        public Task<Enhed> ReadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(Enhed[] obj)
        {
            await _Context.Enheds.AddRangeAsync(obj);
            await _Context.SaveChangesAsync();
        }
        public async Task<Enhed> UpdateAsync(Enhed obj)
        {
            Enhed Old = await _Context.Enheds.FirstOrDefaultAsync(x => x.Id == obj.Id);
            Old.Enheden = obj.Enheden;
            _Context.Enheds.Update(Old);
            await _Context.SaveChangesAsync();
            return Old;
        }
        public async Task Delete(int search)
        {
            _Context.Enheds.Remove(await _Context.Enheds.FirstOrDefaultAsync(x => x.Id == search));
            await _Context.SaveChangesAsync();
        }
    }
}
