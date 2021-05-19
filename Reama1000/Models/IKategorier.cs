using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface IKategorier<TEnhder, TKategorier, TProdukter, TLeveandør> : IItem where TKategorier : IKategorier<TEnhder, TKategorier, TProdukter, TLeveandør> where TProdukter:IProdukter<TEnhder, TKategorier, TProdukter, TLeveandør>where TEnhder: IEnhed where TLeveandør : ILeveandør
    {
        string Beskrivelse { get; set; }
        List<TKategorier> Subkategorier { get; set; }
        List<TProdukter> Produkters { get; set; }
    }
}
