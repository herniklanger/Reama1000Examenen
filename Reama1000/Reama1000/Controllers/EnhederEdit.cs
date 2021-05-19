using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reama1000.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reama1000.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnhederEdit : ControllerBase
    {

        [HttpGet]
        public List<Enhed> Get()
        {
            return null;
        }
        [HttpGet]
        public Enhed Get([FromQuery] int Id)
        {

            return null;
        }
        [HttpPut]
        public Enhed Put([FromBody]Enhed Update)
        {
            return null;

        }
        [HttpPost]
        public Enhed Post([FromBody] Enhed Create)
        {

            return null;
        }
        [HttpDelete]
        public HttpResponse Delete([FromQuery]int Id)
        {

            return null;
        }
    }
}
