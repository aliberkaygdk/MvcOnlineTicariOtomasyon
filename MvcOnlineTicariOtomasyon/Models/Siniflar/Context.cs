using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Context: DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cariler> Cariler { get; set; }
        public DbSet<Departman> Departman { get; set; }
        public DbSet<FaturaKalem> FaturaKalem { get; set; }
        public DbSet<Faturalar> Faturalar { get; set; }
        public DbSet<Gider> Gider { get; set; }
        public DbSet<Kategori> Kategories { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<SatisHareket> SatisHareket { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Detay> Detays { get; set; }
        public DbSet<Yapilacak> Yapilacaks { get; set; }
    }
}