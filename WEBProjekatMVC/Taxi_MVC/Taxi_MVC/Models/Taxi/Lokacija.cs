using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public class Lokacija
    {
        public double XCoord { get; set; }
        public double YCoord { get; set; }
        public Adresa AdresaLok { get; set; }
    }
}