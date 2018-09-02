using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public class Komentar
    {
        [Key]
        [Display(Name ="Datum objave")]
        public String KomentarID { get; set; }
        public String Opis { get; set; }
        public String Ocena { get; set; }
        public String VlasnikKomentara { get; set; }
        public String KomentarisanaVoznja { get; set; }

        public Komentar()
        {

        }

        public Komentar(String id)
        {
            KomentarID = id;
        }
    }
}