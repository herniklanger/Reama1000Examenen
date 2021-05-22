using DataBase.CRUD;
using DataBase.Interfaces;
using DataBase.Models;
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
    public class LevendørController : ControllerBase
    {
        IDataBase<Leveandør, Guid> _Database { get; set; }
        public LevendørController(IDataBase<Leveandør, Guid> database)
        {
            _Database = database;
        }
        [HttpGet]
        public async Task<List<Leveandør>> Get()
        {
            return await _Database.ReadAllAsync();
        }
        [HttpGet("{Id}")]
        public async Task<Leveandør> Get( Guid Id)
        {
            return (await _Database.ReadAsync(x=> x.Id == Id))[0];
        }
        [HttpPut]
        public async Task<Leveandør> Put([FromBody] Leveandør Update)
        {
            return await _Database.UpdateAsync(Update);
        }
        [HttpPost]
        public async void PostAsync([FromBody] Leveandør Create)
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