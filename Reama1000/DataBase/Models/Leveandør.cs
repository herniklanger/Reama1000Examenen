using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Leveandør : ILeveandør
    {
        [Key]
        [JsonIgnore]
        public int InternalId { get; set; }
        public Guid Id { get; set; }
        public string Navn { get; set; }
        public string Adresse { get; set; }
        public int Postnummer { get; set; }
        public string Email { get; set; }
        public string Telefonnummer { get; set; }
    }
}
