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

        /*private void IspisVoznje()
        {
            StreamWriter writer4 = new StreamWriter(path + "packages/Voznje.txt");

            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                writer4.WriteLine(kv.Value.LokacijaDolaskaTaxi.XCoord + " " + kv.Value.LokacijaDolaskaTaxi.YCoord + " " + kv.Value.LokacijaDolaskaTaxi.AdresaLok.Ulica + " " + kv.Value.LokacijaDolaskaTaxi.AdresaLok.Broj + " " + kv.Value.LokacijaDolaskaTaxi.AdresaLok.Mesto + " " + kv.Value.LokacijaDolaskaTaxi.AdresaLok.PozivniBroj + " " + kv.Value.DatumVremePorudzbine.ToString() + " " + kv.Value.StatusVoznje + " " + kv.Value.Musterija.KorisnickoIme + " " + kv.Value.Musterija.Lozinka + " " + kv.Value.Musterija.Ime + " " + kv.Value.Musterija.Prezime + " " + kv.Value.Musterija.Pol + " " + kv.Value.Musterija.Email + " " + kv.Value.Musterija.JMBG + " " + kv.Value.Musterija.Telefon);
            }
        }*/

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

        [HttpPost]
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

        public ActionResult Logout()
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
            auto.VozacAuto = voz;
            voz.AutoVozac = auto;
            lok.AdresaLok = adr;
            voz.LokVozac = lok;
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
            //IspisVoznje();
            return View("PregledSvihVoznjiMusterija");
        }

        [HttpPost]
        public ActionResult Otkazivanje(string date)
        {
            bool test = false;
            if(getVoznje[date].StatusVoznje == EStatus.KREIRANA)
            {
                getVoznje[date].StatusVoznje = EStatus.OTKAZANA;
                test = true;
            }
            if(test)
            {
                ViewBag.voznja = getVoznje[date];
                return View("Komentar");
            }
            else
            {
                return View("OtkazivanjeError");
            }
        }

        public ActionResult Pregledaj()
        {
            return View("PregledSvihVoznjiMusterija");
        }

        public ActionResult GoToHome()
        {
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                if(kv.Value.Musterija.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
                {
                    i++;
                }
            }
            ViewBag.broj = i;
            return View("HomepageMusterija");
        }

        public ActionResult GoToHomeAdmin()
        {
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Dispecer.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
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

        public ActionResult GoToHomeVozac()
        {
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
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
        
        [HttpPost]
        public ActionResult IzmeniVoznju(string date)
        {
            ViewBag.voznja = getVoznje[date];
            return View("IzmenaVoznje");
        }

        [HttpPost]
        public ActionResult IzmenaVoznje(Lokacija lok, Adresa adr, ETipAutomobila tip, string date)
        {
            bool test = false;
            if(getVoznje[date].StatusVoznje == EStatus.KREIRANA)
            {
                lok.AdresaLok = adr;
                Voznja v = new Voznja();
                v.LokacijaDolaskaTaxi = lok;
                v.DatumVremePorudzbine = DateTime.Parse(date);
                if(!tip.Equals(""))
                {
                    v.ZeljeniTipAuto = tip;
                }
                v.StatusVoznje = EStatus.KREIRANA;
                v.Musterija = (Korisnik)Session["Ulogovan"];
                getVoznje[date] = v;
                test = true;
            }

            if (test)
                return View("PregledSvihVoznjiMusterija");
            else
                return View("OtkazivanjeError");
        }
        [HttpPost]
        public ActionResult Komentarisi(int ocena, string date, string komentar)
        {
            getVoznje[date].KomentarNaVoznje = new Komentar();
            getVoznje[date].KomentarNaVoznje.DatumObjave = DateTime.Now;
            getVoznje[date].KomentarNaVoznje.Opis = komentar;
            getVoznje[date].KomentarNaVoznje.OstavljenoZaVoznju = getVoznje[date];
            getVoznje[date].KomentarNaVoznje.Ocena = ocena;

            try
            {
                getVoznje[date].KomentarNaVoznje.OstavioKorisnik = (Vozac)Session["Ulogovan"];
                int i = 0;
                foreach(KeyValuePair<string,Voznja> kv in getVoznje)
                {
                    try
                    {
                        if(kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
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
            catch
            {
                getVoznje[date].KomentarNaVoznje.OstavioKorisnik = (Korisnik)Session["Ulogovan"];
                return View("PregledSvihVoznjiMusterija");
            }
        }
        [HttpPost]
        public ActionResult OstavljanjeKomentara(string date)
        {
            ViewBag.voznja = getVoznje[date];
            return View("Komentar");
        }

        [HttpPost]
        public ActionResult PrikaziVoznju(string date)
        {
            ViewBag.voznja = getVoznje[date];
            return View("PrikazVoznje");
        }
        
        public ActionResult GoToKreiranjeVoznje()
        {
            ViewBag.vozaci = getVozaci;
            return View("KreirajVoznju");
        }

        public ActionResult KreirajVoznju(Lokacija lok, Adresa adr, ETipAutomobila tip, string vozac)        
        {
            lok.AdresaLok = adr;
            Voznja v = new Voznja();
            v.LokacijaDolaskaTaxi = lok;
            v.DatumVremePorudzbine = DateTime.Now;
            v.ZeljeniTipAuto = tip;
            v.StatusVoznje = EStatus.FORMIRANA;
            v.Dispecer = (Korisnik)Session["Ulogovan"];
            v.Vozac = getVozaci[vozac];
            getVozaci[vozac].Zauzet = true;
            v.KomentarNaVoznje = new Komentar();
            v.KomentarNaVoznje.OstavioKorisnik = new Korisnik();
            v.Musterija = new Korisnik();
            v.Musterija.KorisnickoIme = "";
            getVoznje.Add(v.DatumVremePorudzbine.ToString(), v);
            return View("HomepageAdministrator");
        }

        public ActionResult GoToProveraVoznji()
        {
            ViewBag.voznje = getVoznje;
            return View("ProveriVoznje");
        }
        [HttpPost]
        public ActionResult DodeliVoznju(string date)
        {
            getVoznje[date].Dispecer = (Korisnik)Session["Ulogovan"];
            ViewBag.voznja = getVoznje[date].DatumVremePorudzbine.ToString();
            ViewBag.vozaci = getVozaci;
            ViewBag.auto = getVoznje[date].ZeljeniTipAuto;
            return View("DodeliVozacaVoznji");
        }
        [HttpPost]
        public ActionResult DodelaVozacaVoznji(string vozac, string date)
        {
            getVozaci[vozac].Zauzet = true;
            getVoznje[date].Vozac = getVozaci[vozac];
            getVoznje[date].StatusVoznje = EStatus.OBRADJENA;
            return View("HomepageAdministrator");
        }

        public ActionResult GoToProveraVoznjiVozac()
        {
            if(((Vozac)Session["Ulogovan"]).Zauzet)
            {
                return View("VozacPreuzimaError");
            }
            ViewBag.voznje = getVoznje;
            ViewBag.tip = ((Vozac)Session["Ulogovan"]).AutoVozac.TipAutomobila;
            return View("ProveriVoznjeVozac");
        }
        [HttpPost]
        public ActionResult DodeliVoznjuVozac(string date)
        {
            getVoznje[date].Vozac = (Vozac)Session["Ulogovan"];
            getVozaci[((Vozac)Session["Ulogovan"]).KorisnickoIme].Zauzet = true;
            getVoznje[date].StatusVoznje = EStatus.PRIHVACENA;
            int i = 0;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
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

        public ActionResult GoToPromenaStatusaVoznje()
        {
            if (((Vozac)Session["Ulogovan"]).Zauzet)
                return View("PromeniStatusVoznje");
            return View("PromeniStatusVoznjeError");
        }

        [HttpPost]
        public ActionResult PromenaStatusaVoznje(EStatus status)
        {
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                if(kv.Value.Vozac.KorisnickoIme != null)
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals(((Vozac)Session["Ulogovan"]).KorisnickoIme) && !(kv.Value.StatusVoznje == EStatus.USPESNA || kv.Value.StatusVoznje == EStatus.NEUSPESNA))
                    {
                        ((Vozac)Session["Ulogovan"]).Zauzet = false;
                        if(status == EStatus.NEUSPESNA)
                        {
                            kv.Value.StatusVoznje = status;
                            ViewBag.voznja = kv.Value;
                            return View("Komentar");
                        }
                        else
                        {
                            return View("OdredisteUnos");
                        }
                    }
                }
            }
            int i = 0;
            foreach(KeyValuePair<string, Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals((((Korisnik)Session["Ulogovan"]).KorisnickoIme)))
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

        [HttpPost]
        public ActionResult OdredisteIznos(Lokacija lok, Adresa adr, double cena)
        {
            lok.AdresaLok = adr;
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                if(kv.Value.Vozac.KorisnickoIme != null)
                {
                    if(kv.Value.Vozac.KorisnickoIme.Equals(((Vozac)Session["Ulogovan"]).KorisnickoIme) && (kv.Value.StatusVoznje == EStatus.PRIHVACENA || kv.Value.StatusVoznje == EStatus.OBRADJENA || kv.Value.StatusVoznje == EStatus.FORMIRANA ))
                    {
                        kv.Value.StatusVoznje = EStatus.USPESNA;
                        kv.Value.Odrediste = lok;
                        kv.Value.Iznos = cena;
                        break;
                    }
                }
            }

            return View("HomepageVozac");
        }

        public ActionResult GoToSveVoznje()
        {
            ViewBag.voznje = getVoznje;
            return View("IzlistajSveVoznje");
        }
        
        public ActionResult FilterMusterija(EStatus status)
        {
            Dictionary<string, Voznja> voznje = new Dictionary<string, Voznja>();
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.StatusVoznje == status && kv.Value.Musterija.KorisnickoIme.Equals(((Korisnik)Session["Ulogovan"]).KorisnickoIme))
                    {
                        voznje.Add(kv.Key, kv.Value);
                    }
                }
                catch
                {

                }
            }

            ViewBag.voznje = voznje;
            ViewBag.broj = voznje.Count();
            return View("Filtrirano");
        }

        public ActionResult SortiranjeMusterija(string sortiranje)
        {
            Dictionary<string, Voznja> voznje = new Dictionary<string, Voznja>();
            if(sortiranje.Equals("ocena"))
            {
                voznje = getVoznje.ToList().OrderByDescending(o => o.Value.KomentarNaVoznje.Ocena).ToDictionary(x => x.Key, y => y.Value);
            }
            else
            {
                voznje = getVoznje.ToList().OrderByDescending(o => o.Value.DatumVremePorudzbine).ToDictionary(x => x.Key, y => y.Value);
            }

            ViewBag.voznje = voznje;
            ViewBag.broj = voznje.Count();
            return View("Filtrirano");
        }

        public ActionResult Pretraga()
        {
            return View("Pretraga");
        }

        [HttpPost]
        public ActionResult PretragaParametri(int ocena1, int ocena2, int cena1, int cena2, DateTime? datum1 = null, DateTime? datum2 = null)
        {
            Dictionary<string, Voznja> voznje = new Dictionary<string, Voznja>(getVoznje);
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Musterija.KorisnickoIme.Equals(((Korisnik)Session["Ulogovan"]).KorisnickoIme))
                    {
                        if(datum1 == null && datum2 != null && datum2.Value.Date < kv.Value.DatumVremePorudzbine.Date)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(datum1 != null && datum2 == null && datum1.Value.Date > kv.Value.DatumVremePorudzbine.Date)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(datum1 != null && datum2 != null && (datum1.Value.Date > kv.Value.DatumVremePorudzbine.Date || datum2.Value.Date < kv.Value.DatumVremePorudzbine.Date))
                        {
                            voznje.Remove(kv.Key);
                        }
                        if(ocena1 == 0 && ocena2 != 0 && ocena2 < kv.Value.KomentarNaVoznje.Ocena)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(ocena1 != 0 && ocena2 == 0 && ocena1 > kv.Value.KomentarNaVoznje.Ocena)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(ocena1 != 0 && ocena2 != 0 && (ocena1 > kv.Value.KomentarNaVoznje.Ocena || ocena2 < kv.Value.KomentarNaVoznje.Ocena))
                        {
                            voznje.Remove(kv.Key);
                        }
                        if(cena1 == 0 && cena2 != 0 && cena2 < kv.Value.Iznos)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(cena1 != 0 && cena2 == 0 && cena1 > kv.Value.Iznos)
                        {
                            voznje.Remove(kv.Key);
                        }
                        else if(cena1 != 0 && cena2 != 0 && (cena1 > kv.Value.Iznos || cena2 < kv.Value.Iznos))
                        {
                            voznje.Remove(kv.Key);
                        }

                    }
                }
                catch
                {

                }
            }
            ViewBag.voznje = voznje;
            ViewBag.broj = voznje.Count();
            return View("Filtrirano");
        }

        public ActionResult PretragaAdmin()
        {
            return View("PretragaAdmin");
        }

        [HttpPost]
        public ActionResult PretragaParametriAdmin(string vozacIme, string vozacPrezime, string musterijaIme, string musterijaPrezime)        
        {
            if (vozacIme == null)
                vozacIme = "";
            if (vozacPrezime == null)
                vozacPrezime = "";
            if (musterijaIme == null)
                musterijaIme = "";
            if (musterijaPrezime == null)
                musterijaPrezime = "";

            Dictionary<string, Voznja> voznje = new Dictionary<string, Voznja>();
            foreach(KeyValuePair<string,Voznja> kv in getVoznje)
            {
                try
                {
                    if(kv.Value.Vozac.Ime.Equals(vozacIme) || kv.Value.Vozac.Prezime.Equals(vozacPrezime) || kv.Value.Musterija.Ime.Equals(musterijaIme) || kv.Value.Musterija.Prezime.Equals(musterijaPrezime))
                    {
                        voznje.Add(kv.Key, kv.Value);
                    }
                }
                catch { }
            }
            ViewBag.voznje = voznje;
            ViewBag.broj = voznje.Count();
            return View("FiltriranoAdmin");
        }

    }
}