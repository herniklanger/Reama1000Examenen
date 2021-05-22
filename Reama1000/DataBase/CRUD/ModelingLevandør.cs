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
    public class ModelingLeveandør : IDataBase<Leveandør, Guid>
    {
        Reama1000Context _Context { get; set; }

        public ModelingLeveandør(Reama1000Context context)
        {
            _Context = context;
        }
        public async Task<List<Leveandør>> ReadAllAsync()
        {
            return _Context.Leveandørs.ToList();
        }
        public async Task<List<Leveandør>> ReadAsync(Func<Leveandør, bool> search)
        {
            return _Context.Leveandørs.ToList().FindAll(x => search(x));
        }
        public async Task CreateAsync(Leveandør obj)
        {
            obj.Id = Guid.NewGuid();
            await _Context.Leveandørs.AddAsync(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task CreateAsync(Leveandør[] obj)
        {
            await _Context.Leveandørs.AddRangeAsync(obj);
            await _Context.SaveChangesAsync();
        }
        public async Task<Leveandør> UpdateAsync(Leveandør obj)
        {
            Leveandør Old = await _Context.Leveandørs.FirstOrDefaultAsync(x => x.Id == obj.Id);
            Old.Navn = obj.Navn ?? Old.Navn;
            Old.Adresse = obj.Adresse ?? Old.Adresse;
            Old.Email = obj.Email ?? Old.Email;
            Old.Postnummer = obj.Postnummer < 1000 ? Old.Postnummer:obj.Postnummer;
            Old.Telefonnummer = obj.Telefonnummer ?? Old.Telefonnummer;
            _Context.Leveandørs.Update(Old);
            await _Context.SaveChangesAsync();
            return Old;
        }
        public async Task Delete(Guid search)
        {
            _Context.Leveandørs.Remove(await _Context.Leveandørs.FirstOrDefaultAsync(x => x.Id == search));
            await _Context.SaveChangesAsync();
        }
    }
}
