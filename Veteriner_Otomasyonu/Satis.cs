using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veteriner_Otomasyonu
{
    [Table("Tablo_Satis")]
    public class Satis
    {
        [Key]
        public int Satis_Id { get; set; }

        [Required]
        public DateTime Satis_Tarihi { get; set; }

        [Required]
        public int Miktar { get; set; }

        [Required]
        public decimal Toplam_Tutar { get; set; }

        public int Urun_Id { get; set; }
        public virtual Urun Urun { get; set; }

        public int Sahip_Id { get; set; }
        public virtual Sahip Sahip { get; set; }
    }
}
