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
    public class KategorieController : ControllerBase
    {
        IDataBase<Kategorier, Guid> _Database { get; set; }
        public KategorieController(IDataBase<Kategorier, Guid> database)
        {
            _Database = database;
        }
        [HttpGet]
        public async Task<List<Kategorier>> Get()
        {
            return await _Database.ReadAllAsync();
        }
        [HttpGet("{Id}")]
        public async Task<Kategorier> Get( Guid Id)
        {
            var test = await _Database.ReadAsync(x => x.Id == Id);
            test.ForEach(x => x.Produkters.ForEach(p => p.kategoriers = null));
            return test[0];
        }
        [HttpPut]
        public async Task<Kategorier> Put([FromBody]Kategorier Update)
        {
            Kategorier retult = await _Database.UpdateAsync(Update);
            retult.Produkters.ForEach(x => x.kategoriers = null);
            return retult;
        }
        [HttpPost]
        public async void PostAsync([FromBody] Kategorier Create)
        {
            await _Database.CreateAsync(Create);
        }
        [HttpDelete]
        public async void DeleteAsync([FromQuery]Guid Id)
        {
            await _Database.Delete(Id);
        }
    }
}