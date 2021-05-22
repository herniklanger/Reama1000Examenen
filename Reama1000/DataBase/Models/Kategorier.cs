using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class Kategorier : IKategorier<Enhed, Kategorier, Produkter, Leveandør>
    {
        
        [Key][JsonIgnore]
        public int InternalId { get; set; }
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Navn { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [NotMapped]
        public List<Produkter> Produkters { get; set; }
        [JsonIgnore]
        public List<ProduktKategorier> produktKategoriers { get; set; }
    }
}
