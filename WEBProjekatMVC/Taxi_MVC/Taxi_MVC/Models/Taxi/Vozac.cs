using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public class Vozac : Korisnik
    {
        public Lokacija LokVozac { get; set; }
        public Automobil AutoVozac { get; set; }
        public bool Zauzet { get; set; }
    }
}