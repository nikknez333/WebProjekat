using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models
{
    public class Lokacija
    {
        private String xCoord = "0";
        private String yCoord = "0";

        [Required]
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
        [Key]
        public String LokacijaKey { get; set; }
        public String Ulica { get; set; }
        public String Broj { get; set; }
        public String Mesto { get; set; }
        public String PozivniBroj { get; set; }
        
        public Lokacija()
        {

        }

        public Lokacija(String x, String y)
        {
            XCoord = x;
            YCoord = y;
        }




    }
}