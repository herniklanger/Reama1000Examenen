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
        public DbSet<ProduktKategorier> produktKategoriers { get; set; }
        public Reama1000Context()
        {
        }
        public Reama1000Context(DbContextOptions<Reama1000Context> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Reama1000");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Produkter>()
                .HasMany(p => p.kategoriers)
                .WithMany(p => p.Produkters)
                .UsingEntity<ProduktKategorier>(
                    j => j.HasOne(pk => pk.Kategori)
                         .WithMany(t => t.produktKategoriers)
                         .HasForeignKey(pk => pk.KategoriId),
                    j => j.HasOne(pk => pk.Produkt)
                         .WithMany(t => t.produktKategoriers)
                         .HasForeignKey(pk => pk.ProduktId));
        }
    }
}
