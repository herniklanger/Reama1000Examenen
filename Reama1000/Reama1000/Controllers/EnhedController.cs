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
    public class EnhederController : ControllerBase
    {
        IDataBase<Enhed, int> _Database { get; set; }
        public EnhederController(IDataBase<Enhed, int> database)
        {
            _Database = database;
        }
        [HttpGet]
        public async Task<List<Enhed>> Get()
        {
            return await _Database.ReadAllAsync();
        }
        [HttpGet("{Id}")]
        public async Task<Enhed> Get( int Id)
        {
            return (await _Database.ReadAsync(x=> x.Id == Id))[0];
        }
        [HttpPut]
        public async Task<Enhed> Put([FromBody]Enhed Update)
        {
            return await _Database.UpdateAsync(Update);
        }
        [HttpPost]
        public async void PostAsync([FromBody] Enhed Create)
        {
            await _Database.CreateAsync(Create);
        }
        [HttpDelete]
        public async void DeleteAsync([FromQuery]int Id)
        {
            await _Database.Delete(Id);
        }
    }
}