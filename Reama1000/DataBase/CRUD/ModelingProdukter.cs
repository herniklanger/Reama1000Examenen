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
    public class ModelingProdukter : IDataBase<Produkter, Guid>
    {
        Reama1000Context _Context { get; set; }

        public ModelingProdukter(Reama1000Context context)
        {
            _Context = context;
        }
        public async Task<List<Produkter>> ReadAllAsync()
        {
            return _Context.Produkters.Include(p => p.Enhde).ToList();
        }
        public async Task<List<Produkter>> ReadAsync(Func<Produkter, bool> search)
        {
            var context = _Context.Produkters
                .Include(p => p.Leveandør)
                .Include(p => p.Enhde)
                .Include(p => p.produktKategoriers).ThenInclude(x => x.Kategori);
            List<Produkter> produkters = (await context.ToListAsync()).FindAll(x => search(x));
            produkters.ForEach(async x => x.kategoriers = await GetAllKategoriersAsync(x.produktKategoriers));
            return produkters;
                
        }
        public async Task CreateAsync(Produkter obj)
        {
            obj.kategoriers = await FindAllExistingKategories(obj.kategoriers);
            obj.Id = Guid.NewGuid();
            obj.Leveandør = await FindLeveandør(obj.Leveandør);
            await _Context.Produkters.AddAsync(obj);
            await _Context.SaveChangesAsync();
        }

        public async Task CreateAsync(Produkter[] obj)
        {
            foreach(Produkter p in obj)
            {
                p.Id = Guid.NewGuid();
                p.produktKategoriers = p.kategoriers == null ? new List<ProduktKategorier>() : await FindKategoriers(p, p.kategoriers);
                p.Leveandør = await FindLeveandør(p.Leveandør);
            }
            await _Context.Produkters.AddRangeAsync(obj);
            await _Context.SaveChangesAsync();
        }
        public async Task<Produkter> UpdateAsync(Produkter obj)
        {
            Produkter Old = await _Context.Produkters
                .Include(p => p.Leveandør)
                .Include(p => p.produktKategoriers)
                .ThenInclude(pk => pk.Kategori)
                .FirstOrDefaultAsync(x => x.Id == obj.Id);
            Old.Navn = obj.Navn ?? Old.Navn;
            Old.Antal = obj.Antal > 0? Old.Antal : obj.Antal;
            Old.Beskrivelse = obj.Beskrivelse ?? Old.Beskrivelse;
            Old.Enhde = obj.Enhde ?? Old.Enhde;
            Old.produktKategoriers = obj.kategoriers == null ? Old.produktKategoriers : await FindKategoriers(Old, obj.kategoriers);
            Old.Leveandør = obj.Leveandør == null ? Old.Leveandør: await FindLeveandør(obj.Leveandør);
            Old.Mængde = obj.Mængde > 0 ? Old.Mængde : obj.Mængde;
            Old.Pris = obj.Pris > 0 ? Old.Pris : obj.Pris;
            _Context.Produkters.Update(Old);
            await _Context.SaveChangesAsync();
            Old.kategoriers = await GetAllKategoriersAsync(Old.produktKategoriers);
            return Old;
        }
        public async Task Delete(Guid search)
        {
            _Context.Produkters.Remove(await _Context.Produkters.FirstOrDefaultAsync(x => x.Id == search));
            await _Context.SaveChangesAsync();
        }

        private async Task<Leveandør> FindLeveandør(Leveandør leveandør)
        {
            Leveandør existing = await _Context.Leveandørs.FirstOrDefaultAsync(x => x.Id == leveandør.Id);
            if(existing == null)
            {
                existing = leveandør;
                existing.Id = Guid.NewGuid();
            }
            return existing;
        }

        private async Task<List<ProduktKategorier>> FindKategoriers(Produkter produkter, List<Kategorier> kategoriers)
        {
            List<ProduktKategorier> produktKategoriers = new List<ProduktKategorier>(kategoriers.Count);
            foreach (Kategorier kategorier in kategoriers)
            {
                ProduktKategorier existing = await _Context.produktKategoriers.Include(pk => pk.Kategori).FirstOrDefaultAsync(p => p.Kategori.Id == kategorier.Id && p.ProduktId == produkter.InternalId);
                if(existing == null)
                {
                    Kategorier kExisting = await _Context.Kategoriers.FirstOrDefaultAsync(k => k.Id == kategorier.Id);
                    if (kExisting == null)
                    {
                        kExisting = kategorier;
                        kategorier.Id = Guid.NewGuid();
                        _Context.Kategoriers.AddAsync(kExisting);
                        _Context.SaveChanges();
                    }
                    existing = new ProduktKategorier {Produkt = produkter, Kategori = kExisting };
                }
                produktKategoriers.Add(existing);
            }
            return produktKategoriers;
        }
        private async Task<List<Kategorier>> GetAllKategoriersAsync(List<ProduktKategorier> produktKategoriers)
        {
            List<Kategorier> kategoriers = new List<Kategorier>(produktKategoriers.Count);
            foreach (ProduktKategorier pk in produktKategoriers)
            {
                kategoriers.Add(pk.Kategori);
            }
            return kategoriers;
        }
        private async Task<List<Kategorier>> FindAllExistingKategories(List<Kategorier> kategoriers)
        {
            List<Kategorier> result = new List<Kategorier>(kategoriers.Count);
            foreach (Kategorier k in kategoriers)
            {
                result.Add(_Context.Kategoriers.FirstOrDefault(x => x.Id == k.Id) ?? k);
                if(k.Id == Guid.Empty)
                {
                    k.Id = Guid.NewGuid();
                }
            }
            return result;
        }
    }
}
