using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public class Automobil
    {
        [Key]
        [Display(Name ="Oznaka taxi vozila")]
        public String BrojTaxiVozila { get; set; }
        public String Godiste { get; set; }
        public String RegistarskaOznaka { get; set; }
        public ECarType TipAutomobila { get; set; }
        public String Vozac { get; set; }

        public Automobil()
        {

        }

        public Automobil(String brojTaxiVozila)
        {
            BrojTaxiVozila = brojTaxiVozila;
        }

    }
}