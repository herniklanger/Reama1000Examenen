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
    public class ModelingKategorier : IDataBase<Kategorier, Guid>
    {
        Reama1000Context _Context { get; set; }

        public ModelingKategorier(Reama1000Context context)
        {
            _Context = context;
        }

        public async Task<List<Kategorier>> ReadAllAsync()
        {
            return _Context.Kategoriers.ToList();
        }
        public async Task<List<Kategorier>> ReadAsync(Func<Kategorier, bool> search)
        {
            var context = _Context.Kategoriers
                .Include(x => x.produktKategoriers)
                .ThenInclude(pk=>pk.Produkt)
                .ThenInclude(p => p.Enhde);
            List<Kategorier> List = (await context.ToListAsync()).FindAll(x => search(x));
            List.ForEach(async x => x.Produkters = await GetAllProduktsAsync(x.produktKategoriers));
            return List;
        }
        public async Task CreateAsync(Kategorier obj)
        {
            obj.Id = Guid.NewGuid();
            obj.produktKategoriers = obj.Produkters == null ? new List<ProduktKategorier>() : await FindProdukters(obj , obj.Produkters);
            await _Context.Kategoriers.AddAsync(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task CreateAsync(Kategorier[] obj)
        {
            foreach(Kategorier k in obj)
            {
                k.Id = Guid.NewGuid();
                k.produktKategoriers = k.Produkters == null ? new List<ProduktKategorier>(): await FindProdukters(k ,k.Produkters);
            }
            await _Context.Kategoriers.AddRangeAsync(obj);
            await _Context.SaveChangesAsync();
        }
        public async Task<Kategorier> UpdateAsync(Kategorier obj)
        {
            var context = _Context.Kategoriers.Include(x => x.produktKategoriers).ThenInclude(pk => pk.Produkt);
            Kategorier Old = await context.FirstOrDefaultAsync(x => x.Id == obj.Id);
            Old.Navn = obj.Navn ?? Old.Navn;
            Old.Beskrivelse = obj.Beskrivelse ?? Old.Beskrivelse;
            Old.produktKategoriers = obj.Produkters == null ? Old.produktKategoriers: await FindProdukters(obj, obj.Produkters);
            _Context.Kategoriers.Update(Old);
            await _Context.SaveChangesAsync();
            Old.Produkters = await GetAllProduktsAsync(Old.produktKategoriers);
            return Old;
        }
        public async Task Delete(Guid search)
        {
            _Context.Kategoriers.Remove(await _Context.Kategoriers.FirstOrDefaultAsync(x => x.Id == search));
            await _Context.SaveChangesAsync();
        }
        private async Task<List<ProduktKategorier>> FindProdukters(Kategorier kategorier , List<Produkter> produkters)
        {
            List<ProduktKategorier> ProduKategori = new List<ProduktKategorier>(produkters.Count);
            foreach(Produkter produkter in produkters)
            {
                ProduktKategorier existing = await _Context.produktKategoriers.Include(pk => pk.Produkt).FirstOrDefaultAsync(p => p.Produkt.Id == produkter.Id && p.KategoriId == kategorier.InternalId);
                if (existing == null)
                {
                    Produkter pExisting = _Context.Produkters.FirstOrDefault(p => p.Id == produkter.Id);
                    if (pExisting == null)
                    {
                        pExisting = produkter;
                        pExisting.Id = Guid.NewGuid();
                    }
                    existing = new ProduktKategorier { Produkt = pExisting };
                }
                ProduKategori.Add(existing);
            }
            return ProduKategori;
        }
        private async Task<List<Produkter>> GetAllProduktsAsync(List<ProduktKategorier> produktKategoriers)
        {
            List<Produkter> produkters = new List<Produkter>(produktKategoriers.Count);
            foreach(ProduktKategorier p in produktKategoriers)
            {
                produkters.Add(p.Produkt);
            }
            return produkters;
        }
    }
}
