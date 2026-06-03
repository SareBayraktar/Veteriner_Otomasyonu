using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veteriner_Otomasyonu
{
        [Table("Tablo_Urun")]
        public class Urun
        {
            [Key]
            public int Urun_Id { get; set; }

            [Required]
            [MaxLength(100)]
            public string Urun_Adi { get; set; }

            [Required]
            [MaxLength(50)]
            public string Urun_Turu { get; set; }

            [Required]
            public decimal Urun_Fiyati { get; set; }

            [Required]
            public int Stok_Miktari { get; set; }

            public virtual ICollection<Satis> Satislar { get; set; }
        }
}

