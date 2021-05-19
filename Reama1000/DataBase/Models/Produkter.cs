using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Navn { get; set; }
        [Required]
        public string Beskrivelse { get; set; }
        [Required]
        public double Mængde { get; set; }
        [Required]
        public IEnhed Enhde { get; set; }
        [Required]
        public double Pris { get; set; }
        public List<Kategorier> kategoriers { get; set; }
        [Required]
        public int Antal { get; set; }
        [Required]
        public Leveandør Leveandør { get; set; }
    }
}
