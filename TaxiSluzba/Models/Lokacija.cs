using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Lokacija
    {
        public Lokacija()
        {

        }

        public Lokacija(String x, String y)
        {
            this.X = x;
            this.Y = y;
        }

        public string X { get; set; }
        public string Y { get; set; }

        public Adresa AdresaLokacije { get; set; }
    }
}