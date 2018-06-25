using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Komentar
    {
        public Komentar()
        {

        }

        public Komentar(string idKomentar)
        {
            this.IdKomentar = idKomentar;
        }
        public string IdKomentar { get; set; }
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public Korisnik OstavioKorisnik { get; set; }
        public Voznja KomentarisanaVoznja { get; set; }
        public int Ocena { get; set; }
        public Statusi StatusVoznje { get; set; }

    }


}