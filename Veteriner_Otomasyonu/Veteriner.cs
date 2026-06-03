using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veteriner_Otomasyonu
{
    [Table("Tablo_Veteriner")]
    public class Veteriner
    {
        [Key]
        public int Veteriner_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Veteriner_Adi { get; set; }

        [Required]
        [MaxLength(50)]
        public string Veteriner_Soyadi { get; set; }

        [Required]
        [MaxLength(50)]
        public string Veteriner_Uzmanlik { get; set; }

        public virtual ICollection<Muayene> Muayeneler { get; set; }
        
        public string AdSoyad
        {
            get { return Veteriner_Adi + " " + Veteriner_Soyadi; }
        }
    }
}
