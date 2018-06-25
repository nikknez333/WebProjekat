using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Voznja
    {
        public Voznja()
        {

        }

        public DateTime VremePorudzbine { get; set; }
        public Lokacija DolaziNa { get; set; }
        public TipAutomobila Tip { get; set; }
        public Korisnik Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public Korisnik Dispecer { get; set; }
        public Korisnik Vozac { get; set; }
        public int Iznos { get; set; }
        public Komentar Komentar { get; set; }
        public Statusi StatusVoznje { get; set; }
    }

    
}