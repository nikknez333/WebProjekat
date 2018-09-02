using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public class Komentar
    {
        public string Opis { get; set; }
        public DateTime DatumObjave { get; set; }
        public Korisnik OstavioKorisnik { get; set; }
        public Voznja OstavljenoZaVoznju { get; set; }
        public int Ocena { get; set; }
    }
}