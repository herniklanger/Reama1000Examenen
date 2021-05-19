using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public interface ILeveandør : IItem
    {
        string Adresse { get; set; }
        int Postnummer { get; set; }
        string Email { get; set; }
        string Telefonnummer { get; set; }
    }
}
