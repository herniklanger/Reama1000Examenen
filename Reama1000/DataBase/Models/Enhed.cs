using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Enhed : IEnhed
    {
        public int Id { get; set; }
        public string Enheden { get; set; }
    }
}
