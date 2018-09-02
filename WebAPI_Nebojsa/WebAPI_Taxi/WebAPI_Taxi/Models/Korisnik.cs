using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public class Korisnik
    {
        private String xCoord = "0";
        private String yCoord = "0";
        [Key]
        [Display(Name="KorisnickoIme")]
        public String KorisnikID { get; set; }
        [Required]
        public String Password { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        [Required]
        public EGender Gender { get; set; }
        public String EMail { get; set; }
        public String Telephone { get; set; }
        public String IDNumber { get; set; }
        public List<Voznja> Voznje { get; set; }
        [Required]
        public EUloga Uloga { get; set; }
        [ForeignKey("LokacijaKey")]
        public Lokacija LokacijaVozaca { get; set; }
        public ECarType TipAutomobila { get; set; }
        public String AutomobilID { get; set; }
        [ForeignKey("AutomobilID")]
        public Automobil TaxiVozilo { get; set; }
        public String LokacijaKey { get; private set; }
        public String XCoord
        {
            get
            {
                return xCoord;

            }
            set
            {
                xCoord = value;
                LokacijaKey = XCoord + "_" + YCoord;
            }
        }
        public String YCoord
        {
            get
            {
                return yCoord;

            }
            set
            {
                yCoord = value;
                LokacijaKey = XCoord + "_" + YCoord;
            }
        }

        public Korisnik()
        {
            Voznje = new List<Voznja>();
        }

        public Korisnik(String korisnickoIme, String password, EUloga uloga, String LokacijaVozaca_XCoord, String LokacijaVozaca_YCoord)
        {
            Lokacija l = new Lokacija(LokacijaVozaca_XCoord, LokacijaVozaca_YCoord);
            KorisnikID = korisnickoIme;
            Password = password;
            Uloga = uloga;
        }

    }
}