using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veteriner_Otomasyonu
{
    [Table("Tablo_Muayene")]
    public class Muayene
    {
        [Key]
        public int Muayene_Id { get; set; }

        [Required]
        public DateTime Muayene_Tarihi { get; set; }

        [Required]
        [MaxLength(200)]
        public string Teshis { get; set; }

        [Required]
        [MaxLength(200)]
        public string Tedavi { get; set; }

        [Required]
        public decimal Muayene_Ucreti { get; set; }

        public int Hayvan_Id { get; set; }

        public virtual Hayvan Hayvan { get; set; }

        public int Veteriner_Id { get; set; }

        public virtual Veteriner Veteriner { get; set; }
    }
}
