using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Veteriner_Otomasyonu
{
    [Table("Tablo_Hayvan")]
    public class Hayvan
    {

        [Key]
        public int Hayvan_Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Hayvan_Adi { get; set; }

        [Required]
        [MaxLength(30)]
        public string Hayvan_Turu { get; set; }

        [Required]
        [MaxLength(50)]
        public string Hayvan_Cinsi { get; set; }

        public int Sahip_Id { get; set; }

        public virtual Sahip Sahip { get; set; }

        public virtual ICollection<Muayene> Muayeneler { get; set; }
    }
}
