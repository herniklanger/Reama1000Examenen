using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataBase
{
    public class Reama1000Context : DbContext
    {
        public DbSet<Enhed> Enheds { get; set; }
        public DbSet<Kategorier> Kategoriers { get; set; }
        public DbSet<Leveandør> Leveandørs { get; set; }
        public DbSet<Produkter> Produkters { get; set; }
        public Reama1000Context()
        {
        }
        public Reama1000Context(DbContextOptions<Reama1000Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Data Source=LAPTOP-U3V1724K;Initial Catalog=Reama1000;Integrated Security=True");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
