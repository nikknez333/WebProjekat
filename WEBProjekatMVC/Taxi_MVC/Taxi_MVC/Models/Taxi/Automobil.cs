
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Taxi_MVC.Models
{
    public class Automobil
    {
        public Vozac VozacAuto { get; set; }
        public int Godiste { get; set; }
        public string RegistarskaOznaka { get; set; }
        public int BrojVozila { get; set; }
        public ETipAutomobila TipAutomobila { get; set; }
    }
}