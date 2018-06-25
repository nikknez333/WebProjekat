using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Adresa
    {
        public Adresa()
        {

        }

        public string Ulica { get; set; }
        public int Broj { get; set; }
        public string Mesto { get; set; }
        public int PozivniBroj { get; set; }
    }
}