using DataBase.Models;
using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DataBase
{
    public static class Seeding
    {
        static IDataBase<Produkter, Guid> _produkt { get; set; }
        static IDataBase<Kategorier, Guid> _kayegorier { get; set; }
        static IDataBase<Leveandør, Guid> _layegorier { get; set; }
        static IDataBase<Enhed, int> _enhed { get; set; }

        public static async void Seed(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                _produkt = scope.ServiceProvider.GetService<IDataBase<Produkter, Guid>>();
                _kayegorier = scope.ServiceProvider.GetService<IDataBase<Kategorier, Guid>>();
                _layegorier = scope.ServiceProvider.GetService<IDataBase<Leveandør, Guid>>();
                _enhed = scope.ServiceProvider.GetService<IDataBase<Enhed, int>>();
                Kategorier kategorier1 = new Kategorier { Beskrivelse = "Hvede mel", Navn = "Hvedeme", Produkters = new List<Produkter>() };
                Kategorier kategorier2 = new Kategorier { Beskrivelse = "Alle meltyper", Navn = "Mel", Produkters = new List<Produkter>() };
                Leveandør leveandør = new Leveandør { Navn = "", Adresse = "TestSt. 4", Email = "Test@Test.com", Postnummer = 5040, Telefonnummer = "+5056432343" };
                Enhed Kg = new Enhed { Enheden = "Kg" };
                List<Produkter> produkter = new List<Produkter>
                { new Produkter
                    {
                        Antal = 10,
                        Beskrivelse = "Det beste mel på markedet",
                        Mængde = 2,
                        Navn = "Dansk Dansk Hvedeme",
                        Pris = 19.95,
                        Enhde = Kg,
                        Leveandør = leveandør,
                        kategoriers = new List<Kategorier>{ kategorier1 , kategorier2}

                    },
                    new Produkter
                    {
                        Antal = 10,
                        Beskrivelse = "Det beste mel på markedet",
                        Mængde = 2,
                        Navn = "Kara Age",
                        Pris = 10.25,
                        Enhde = Kg,
                        Leveandør = leveandør,
                        kategoriers = new List<Kategorier>{ kategorier1 , kategorier2}

                    }
                };
                foreach (var p in produkter)
                {
                    await _produkt.CreateAsync(p);
                }
            }
        }
    }
}
