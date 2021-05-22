using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IProdukter<TEnhed, TKategorier, TProdukter, TLeveandør> : IItem where TEnhed : IEnhed where TKategorier : IKategorier<TEnhed, TKategorier, TProdukter, TLeveandør> where TProdukter : IProdukter<TEnhed, TKategorier, TProdukter, TLeveandør>where TLeveandør : ILeveandør
    {
        string Beskrivelse { get; set; }
        TEnhed Enhed { get; set; }
        double Mængde { get; set; }
        double Pris { get; set; }
        List<TKategorier> kategoriers { get; set; }
        int Antal { get; set; }
        TLeveandør Leverandør { get; set; }

    }
}
