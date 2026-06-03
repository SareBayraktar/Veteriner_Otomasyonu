using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veteriner_Otomasyonu
{
    public class VeterinerDbContext : DbContext
    {
        public VeterinerDbContext() : base("name=VeterinerDbContext")
        {
        }

        public DbSet<Sahip> Sahipler { get; set; }
        public DbSet<Hayvan> Hayvanlar { get; set; }
        public DbSet<Veteriner> Veterinerler { get; set; }
        public DbSet<Muayene> Muayeneler { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Satis> Satislar { get; set; }
    }
}
