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
    public class Produkter : IProdukter<Enhed, Kategorier, Produkter, Leveandør>
    {
        [Key]
        [JsonIgnore]
        public int InternalId { get; set; }
        
        public Guid Id { get; set; }
        
        public string Navn { get; set; }
        
        public string Beskrivelse { get; set; }
        
        public double Mængde { get; set; }
        
        public Enhed Enhed { get; set; }
        
        public double Pris { get; set; }
        [NotMapped]
        public List<Kategorier> kategoriers { get; set; }
        [JsonIgnore]
        public List<ProduktKategorier> produktKategoriers { get; set; }
        
        public int Antal { get; set; }
        
        public Leveandør Leverandør { get; set; }
    }
}
