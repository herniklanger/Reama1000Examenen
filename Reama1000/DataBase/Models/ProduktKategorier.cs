using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public class ProduktKategorier
    {
        [Key]
        int Id { get; set; }
        public int ProduktId { get; set; }
        public Produkter Produkt { get; set; }

        public int KategoriId { get; set; }
        public Kategorier Kategori { get; set; }
    }
}
