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
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Navn { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public int Postnummer { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefonnummer { get; set; }
    }
}
