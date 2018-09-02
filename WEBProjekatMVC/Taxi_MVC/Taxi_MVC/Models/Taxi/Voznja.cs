using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public class Voznja
    {
        public DateTime DatumVremePorudzbine { get; set; }
        public Lokacija LokacijaDolaskaTaxi { get; set; }
        public ETipAutomobila ZeljeniTipAuto { get; set; }
        public Korisnik Musterija { get; set; }
        public Lokacija Odrediste { get; set; }
        public Korisnik Dispecer { get; set; }
        public Vozac Vozac { get; set; }
        public double Iznos { get; set; }
        public Komentar KomentarNaVoznje { get; set; }
        public EStatus StatusVoznje { get; set; }


    }
}