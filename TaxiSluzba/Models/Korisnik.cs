using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaxiSluzba.Models
{
    public class Korisnik
    {
        public Korisnik()
        {
            Voznje = new List<Voznja>();
        }

        public Korisnik(string username, string password, string ime, string prezime, Polovi pol, string jmbg, int telefon, string email, Uloge uloga, List<Voznja> voznje)
        {
            this.UserID = username;
            this.Password = password;
            this.Ime = ime;
            this.Prezime = prezime;
            this.Pol = pol;
            this.Jmbg = jmbg;
            this.Telefon = telefon;
            this.Email = email;
            this.Uloga = uloga;
            this.Voznje = voznje;
        }

        [Display(Name = "Korisnicko ime")]
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public Polovi Pol { get; set; }
        public string Jmbg { get; set; }
        public int Telefon { get; set; }
        public string Email { get; set; }
        public Uloge Uloga { get; set; }
        public List<Voznja> Voznje { get; set; }
    }
}