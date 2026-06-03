using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Veteriner_Otomasyonu
{
    [Table("Tablo_Sahip")]
    public class Sahip
    {
        [Key]
        public int Sahip_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sahip_Adi { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sahip_Soyadi { get; set; }

        [Required]
        [MaxLength(20)]
        public string Sahip_Telefon { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sahip_Email { get; set; }

        public virtual ICollection<Hayvan> Hayvanlar { get; set; }

        public string AdSoyad
        {
            get { return Sahip_Adi + " " + Sahip_Soyadi; }
        }
    }
}
