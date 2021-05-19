using DataBase.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataBase.CRUD
{
    class ModelingKategorier : IDataBase<Kategorier, Guid>
    {
        Reama1000Context _Context { get; set; }

        public async Task<List<Kategorier>> ReadAllAsync()
        {
            return _Context.Kategoriers.ToList();
        }
        public async Task<List<Kategorier>> ReadAsync(Func<Kategorier, bool> search)
        {
            return _Context.Kategoriers.ToList().FindAll(x => search(x));
        }
        public async Task CreateAsync(Kategorier obj)
        {
            await _Context.Kategoriers.AddAsync(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task CreateAsync(Kategorier[] obj)
        {
            await _Context.Kategoriers.AddRangeAsync(obj);
            await _Context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Kategorier obj)
        {
            Kategorier Old = await _Context.Kategoriers.FirstOrDefaultAsync(x => x.Id == obj.Id);
            Old.Navn = obj.Navn ?? Old.Navn;
            Old.Beskrivelse = obj.Beskrivelse ?? Old.Beskrivelse;
            Old.Subkategorier = obj.Subkategorier ?? Old.Subkategorier;
            Old.Produkters = obj.Produkters ?? Old.Produkters;
        }
        public async Task Delete(Guid search)
        {
            _Context.Kategoriers.Remove(await _Context.Kategoriers.FirstOrDefaultAsync(x => x.Id == search));
            await _Context.SaveChangesAsync();
        }
    }
}
