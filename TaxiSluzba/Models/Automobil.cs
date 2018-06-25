using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Automobil
    {
        public Automobil()
        {

        }

        public Automobil(String idVozila, Korisnik vozacAutomobila)
        {
            this.IdVozila = IdVozila;
            this.VozacAutomobila = vozacAutomobila;
        }

        public Korisnik VozacAutomobila { get; set; }
        public string GodisteAutomobila { get; set; }
        public string RegistarskaOznaka { get; set; }
        public int IdVozila { get; set; }
        public TipAutomobila Tip { get; set; }
    }

}