using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public class Voznja
    {
        private String lokacija_XCoord = "0";
        private String lokacija_YCoord = "0";
        private String odrediste_XCoord = "0";
        private String odrediste_YCoord = "0";

        [Key]
        [Display(Name = "VremeVoznje")]
        public String VoznjaID { get; set; }

       
        public String Lokacija_XCoord
        {
            get
            {
                return lokacija_XCoord;
            }
            set
            {
                lokacija_XCoord = value;
                Lokacija_Key = Lokacija_XCoord + "_" + Lokacija_YCoord;
            }
        }
      
        public String Lokacija_YCoord
        {
            get
            {
                return lokacija_YCoord;
            }
            set
            {
                lokacija_YCoord = value;
                Lokacija_Key = Lokacija_XCoord + "_" + Lokacija_YCoord;
            }
        }

        public String Lokacija_Key { get; private set; }
        [ForeignKey("Lokacija_Key")]
        public Lokacija Lokacija { get; set; }
        public ECarType TipAutomobila { get; set; }
        public Double Iznos { get; set; }

        public String Odrediste_Key { get; private set; }
        public String Odrediste_XCoord
        {
            get
            {
                return odrediste_XCoord;
            }
            set
            {
                odrediste_XCoord = value;
                Odrediste_Key = Odrediste_XCoord + "_" + Odrediste_YCoord;
                if(Odrediste_XCoord == null || Odrediste_YCoord == null)
                {
                    Odrediste_Key = null;
                }
            }
        }

        public String Odrediste_YCoord
        {
            get
            {
                return odrediste_YCoord;
            }
            set
            {
                odrediste_YCoord = value;
                Odrediste_Key = Odrediste_XCoord + "_" + Odrediste_YCoord;
                if (Odrediste_XCoord == null || Odrediste_YCoord == null)
                {
                    Odrediste_Key = null;
                }
            }
        }

        [ForeignKey("Odrediste_Key")]
        public Lokacija Odrediste { get; set; }

        public String MusterijaID { get; set; }
        [ForeignKey("MusterijaID")]
        public Korisnik Musterija { get; set; }

        public String DispecerID { get; set; }
        [ForeignKey("DispecerID")]
        public Korisnik Dispecer { get; set; }

        public String VozacID { get; set; }
        [ForeignKey("VozacID")]
        public Korisnik Vozac { get; set; }

        public String KomentarID { get; set; }
        [ForeignKey("KomentarID")]
        public Komentar KomentarVoznje { get; set; }

        public EStatus StatusVoznje { get; set; } = EStatus.KREIRANA;

        public Voznja()
        {

        }

        public Voznja(String id)
        {
            VoznjaID = id;
        }
    }
}