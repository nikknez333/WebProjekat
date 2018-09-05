using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Taxi_MVC.Models;


namespace Taxi_MVC.Controllers
{
    public class TaxiController : Controller
    {
        private string path = "D:/WEBProjekatMVC/Taxi_MVC/";

        private void IspisVozaca()
        {
            StreamWriter writer2 = new StreamWriter(path + "packages/Vozaci.txt");

            foreach (KeyValuePair<string, Vozac> kv in getVozaci)
            {
                writer2.WriteLine(kv.Value.KorisnickoIme + " " + kv.Value.Lozinka + " " + kv.Value.Ime + " " + kv.Value.Prezime + " " + kv.Value.Pol + " " + kv.Value.Email +
                " " + kv.Value.JMBG + " " + kv.Value.Telefon + " " + kv.Value.AutoVozac.BrojVozila + " " + kv.Value.AutoVozac.Godiste + " " + kv.Value.AutoVozac.RegistarskaOznaka +
                " " + kv.Value.AutoVozac.TipAutomobila + " " + kv.Value.LokVozac.XCoord + " " + kv.Value.LokVozac.YCoord + " " + kv.Value.LokVozac.AdresaLok.Ulica + " " +
                kv.Value.LokVozac.AdresaLok.Broj + " " + kv.Value.LokVozac.AdresaLok.Mesto + " " + kv.Value.LokVozac.AdresaLok.PozivniBroj);
            }

            writer2.Close();
        }

        private void IspisKorisnika()
        {
            StreamWriter writer3 = new StreamWriter(path + "packages/Korisnici.txt");

            foreach (KeyValuePair<string, Korisnik> kv in getKorisnik)
            {
                writer3.WriteLine(kv.Value.KorisnickoIme + " " + kv.Value.Lozinka + " " + kv.Value.Ime + " " + kv.Value.Prezime + " " + kv.Value.Pol + " " + kv.Value.Email +
                " " + kv.Value.JMBG + " " + kv.Value.Telefon);
            }

            writer3.Close();
        }

        private Dictionary<string, Korisnik> getKorisnik
        {
            get
            {
                return (Dictionary<string, Korisnik>)Session["RegistrovaniKorisnici"];
            }
        }

        private Dictionary<string, Vozac> getVozaci
        {
            get
            {
                return (Dictionary<string, Vozac>)Session["Vozaci"];
            }
        }

        private Dictionary<string, Korisnik> getAdministratori
        {
            get
            {
                return (Dictionary<string, Korisnik>)Session["Administratori"];
            }
        }

        private Dictionary<string, Voznja> getVoznje
        {
            get
            {
                return (Dictionary<string, Voznja>)Session["Voznje"];
            }
        }

        // GET: Taxi
        public ActionResult Index()
        {
            if (Session["Ulogovan"] != null)
                return View("HomepageMusterija");
            return View();
        }

        public ActionResult GoToRegistration()
        {
            return View("Registration");
        }

        public ActionResult GoToLogin()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Registration(Korisnik k)
        {
            k.Uloga = EUloga.MUSTERIJA;
            if (getKorisnik.ContainsKey(k.KorisnickoIme) || getAdministratori.ContainsKey(k.KorisnickoIme) || getVozaci.ContainsKey(k.KorisnickoIme))
            {
                ViewBag.korisnik = k;
                return View("RegistrationError");
            }
            else
            {
                getKorisnik.Add(k.KorisnickoIme, k);
            }
            IspisKorisnika();

            return View("Index");
        }


        public ActionResult Login(string korisnickoIme, string lozinka)
        {
            int i = 0;

            if (!getKorisnik.ContainsKey(korisnickoIme))
            {
                if (getAdministratori.ContainsKey(korisnickoIme))
                {
                    if (!getAdministratori[korisnickoIme].Lozinka.Equals(lozinka))
                    {
                        return View("LozinkaError");
                    }
                    Session["Ulogovan"] = getAdministratori[korisnickoIme];
                    i = 0;
                    foreach (KeyValuePair<string, Voznja> kv in getVoznje)
                    {
                        try
                        {
                            if (kv.Value.Dispecer.KorisnickoIme.Equals(getAdministratori[korisnickoIme].KorisnickoIme))
                            {
                                i++;
                            }
                        }
                        catch
                        {

                        }
                    }
                    ViewBag.broj = i;
                    return View("HomepageAdministrator");
                }
                if (getVozaci.ContainsKey(korisnickoIme))
                {
                    if (!getVozaci[korisnickoIme].Lozinka.Equals(lozinka))
                    {
                        return View("LozinkaError");
                    }
                    Session["Ulogovan"] = getVozaci[korisnickoIme];
                    i = 0;
                    foreach (KeyValuePair<string, Voznja> kv in getVoznje)
                    {
                        try
                        {
                            if (kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
                            {
                                i++;
                            }
                        }
                        catch
                        {

                        }
                    }
                    ViewBag.broj = i;
                    return View("HomepageVozac");
                }

                return View("KorisnickoImeError");
            }

            if (!getKorisnik[korisnickoIme].Lozinka.Equals(lozinka))
            {
                return View("LozinkaError");
            }

            Session["Ulogovan"] = getKorisnik[korisnickoIme];
            i = 0;
            foreach (KeyValuePair<string, Voznja> kv in getVoznje)
            {
                try
                {
                    if (kv.Value.Musterija.KorisnickoIme.Equals(getKorisnik[korisnickoIme].KorisnickoIme))
                    {
                        i++;
                    }
                }
                catch
                {

                }
            }
            ViewBag.broj = i;
            return View("HomepageMusterija");
        }

        public ActionResult Izmena()
        {
            return View("Izmena");
        }

        public ActionResult IzmenaVozac()
        {
            return View("IzmenaVozac");
        }

        public ActionResult IzmenaAdmin()
        {
            return View("IzmenaAdmin");
        }

        public ActionResult Odjava()
        {
            Session["Ulogovan"] = null;
            return View("Index");
        }

        public ActionResult Izmeni(Korisnik k)
        {
            if (!((Korisnik)Session["Ulogovan"]).KorisnickoIme.Equals(k.KorisnickoIme) && (getKorisnik.ContainsKey(k.KorisnickoIme) || getAdministratori.ContainsKey(k.KorisnickoIme) || getVozaci.ContainsKey(k.KorisnickoIme)))
            {
                ViewBag.korisnik = k;
                return View("RegistrationError2");
            }
            if (!((Korisnik)Session["Ulogovan"]).KorisnickoIme.Equals(k.KorisnickoIme))
            {
                getKorisnik.Remove(((Korisnik)Session["Ulogovan"]).KorisnickoIme);
            }
            k.Uloga = EUloga.MUSTERIJA;
            getKorisnik[k.KorisnickoIme] = k;
            Session["Ulogovan"] = getKorisnik[k.KorisnickoIme];
            int i = 0;
            foreach (KeyValuePair<string, Voznja> kv in getVoznje)
            {
                if (kv.Value.Musterija.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
                {
                    i++;
                }
            }
            IspisKorisnika();
            ViewBag.broj = i;
            return View("HomepageMusterija");
        }

        public ActionResult IzmeniVozac(Vozac voz, Automobil auto, Lokacija lok, Adresa adr)
        {
            auto.VozacAuto = voz;
            voz.AutoVozac = auto;
            lok.AdresaLok = adr;
            voz.LokVozac = lok;
            voz.Zauzet = false;
            voz.Uloga = EUloga.VOZAC;

            if (!((Vozac)Session["Ulogovan"]).KorisnickoIme.Equals(voz.KorisnickoIme) && (getKorisnik.ContainsKey(voz.KorisnickoIme) || getAdministratori.ContainsKey(voz.KorisnickoIme) || getVozaci.ContainsKey(voz.KorisnickoIme)))
            {
                ViewBag.vozac = voz;
                return View("IzmenaVozacError");
            }
            if (!((Vozac)Session["Ulogovan"]).KorisnickoIme.Equals(voz.KorisnickoIme))
            {
                getVozaci.Remove(((Vozac)Session["Ulogovan"]).KorisnickoIme);

            }
            getVozaci[voz.KorisnickoIme] = voz;
            Session["Ulogovan"] = getVozaci[voz.KorisnickoIme];
            int i = 0;

            foreach (KeyValuePair<string, Voznja> kv in getVoznje)
            {
                try
                {
                    if (kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
                    {
                        i++;
                    }
                }
                catch
                {

                }
            }
            IspisVozaca();
            ViewBag.broj = i;
            return View("HomepageVozac");
        }

        public ActionResult IzmeniAdmin(Korisnik k)
        {
            if (!((Korisnik)Session["Ulogovan"]).KorisnickoIme.Equals(k.KorisnickoIme) && (getKorisnik.ContainsKey(k.KorisnickoIme) || getAdministratori.ContainsKey(k.KorisnickoIme) || getVozaci.ContainsKey(k.KorisnickoIme)))
            {
                ViewBag.korisnik = k;
                return View("RegistrationError");
            }
            if (!((Korisnik)Session["Ulogovan"]).KorisnickoIme.Equals(k.KorisnickoIme))
            {
                getAdministratori.Remove(((Korisnik)Session["Ulogovan"]).KorisnickoIme);
            }
            k.Uloga = EUloga.DISPECER;
            getAdministratori[k.KorisnickoIme] = k;
            Session["Ulogovan"] = getAdministratori[k.KorisnickoIme];
            int i = 0;
            foreach (KeyValuePair<string, Voznja> kv in getVoznje)
            {
                try
                {
                    if (kv.Value.Dispecer.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
                    {
                        i++;
                    }
                }
                catch
                {

                }
            }
            ViewBag.broj = i;
            return View("HomepageAdministrator");
        }

        public ActionResult GoToKreiranjeVozaca()
        {
            return View("KreiranjeVozaca");
        }

        [HttpPost]
        public ActionResult KreiranjeVozaca(Vozac voz, Automobil auto, Lokacija lok, Adresa adr)
        {
            voz.AutoVozac = auto;
            auto.VozacAuto = voz;
            lok.AdresaLok = adr;
            voz.Zauzet = false;
            voz.Uloga = EUloga.VOZAC;

            if(getKorisnik.ContainsKey(voz.KorisnickoIme) || getAdministratori.ContainsKey(voz.KorisnickoIme) || getVozaci.ContainsKey(voz.KorisnickoIme))
            {
                ViewBag.vozac = voz;
                return View("KreiranjeVozacaError");
            }
            getVozaci.Add(voz.KorisnickoIme, voz);
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Dispecer.KorisnickoIme.Equals(((Korisnik)Session["Ulogovan"]).KorisnickoIme))
                    {
                        i++;
                    }
                }
                catch
                {

                }
            }
            IspisVozaca();
            ViewBag.broj = i;
            return View("HomepageAdministrator");
        }

        public ActionResult PromeniLokaciju()
        {
            return View("PromenaLokacije");
        }
        [HttpPost]
        public ActionResult PromenaLokacije(string log,Lokacija lok, Adresa adr)
        {
            lok.AdresaLok = adr;
            getVozaci[log].LokVozac = lok;
            Session["Ulogovan"] = getVozaci[log];
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals(((Korisnik)Session["Ulogovan"]).KorisnickoIme))
                    {
                        i++;
                    }
                }
                catch
                {

                }
            }
            ViewBag.broj = i;
            return View("HomepageVozac");
        }

        public ActionResult MusterijaZahtev()
        {
            return View("MusterijaZahtev");
        }

        [HttpPost]
        public ActionResult KreiranjeVoznje(Lokacija lok, Adresa adr, ETipAutomobila tip)
        {
            lok.AdresaLok = adr;
            Voznja v = new Voznja();
            v.LokacijaDolaskaTaxi = lok;
            v.DatumVremePorudzbine = DateTime.Now;
            v.ZeljeniTipAuto = tip;
            v.StatusVoznje = EStatus.KREIRANA;
            v.Musterija = (Korisnik)Session["Ulogovan"];
            v.Vozac = new Vozac();
            v.KomentarNaVoznje = new Komentar();
            getVoznje.Add(v.DatumVremePorudzbine.ToString(), v);
            return View("PregledSvihVoznjiMusterije");
        }


    }
}