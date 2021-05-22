using DataBase.CRUD;
using DataBase.Models;
using DataBase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reama1000.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdukterController : ControllerBase
    {
        IDataBase<Produkter, Guid> _Database { get; set; }
        public ProdukterController(IDataBase<Produkter, Guid> Leveandør)
        {
            _Database = Leveandør;
        }
        [HttpGet]
        public async Task<List<Produkter>> Get()
        {
            return await _Database.ReadAllAsync();
        }
        [HttpGet("{Id}")]
        public async Task<Produkter> Get( Guid Id)
        {
            List <Produkter> prod = await _Database.ReadAsync(x => x.Id == Id);
            prod.ForEach(x => x.kategoriers.ForEach(x=>x.Produkters = null));
            return (prod)[0];
        }
        [HttpPut]
        public async Task<Produkter> Put([FromBody] Produkter Update)
        {
            Produkter result = await _Database.UpdateAsync(Update);
            result.kategoriers.ForEach(x => x.Produkters = null);
            return result;
        }
        [HttpPost]
        public async Task<Produkter> PostAsync([FromBody] Produkter Create)
        {
            await _Database.CreateAsync(Create);
            Create.kategoriers.ForEach(x => x.Produkters = null);
            return Create;
        }
        [HttpDelete]
        public async void DeleteAsync([FromQuery]Guid Id)
        {
            await _Database.Delete(Id);
        }
    }
}