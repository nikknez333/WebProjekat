using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Taxi_MVC.Models;

namespace Taxi_MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string path = "D:/WEBProjekatMVC/Taxi_MVC/";
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

         

        }

        protected void Session_Start()
        {
            Dictionary<string, Korisnik> administratori = new Dictionary<string, Korisnik>();
            string line;
            if (!File.Exists(path + "packages/Administratori.txt"))
            {
                File.Create(path + "packages/Administratori.txt");
            }

            StreamReader reader = new StreamReader(path + "packages/Administratori.txt");
            while ((line = reader.ReadLine()) != null)
            {
                Korisnik administrator = new Korisnik();
             
                administrator.KorisnickoIme = line.Split(' ')[0];
                administrator.Lozinka = line.Split(' ')[1];
                administrator.Ime = line.Split(' ')[2];
                administrator.Prezime = line.Split(' ')[3];
                if (line.Split(' ')[4].ToLower().Equals("m"))
                {
                    administrator.Pol = EPol.MUSKI;
                }
                else
                {
                    administrator.Pol = EPol.ZENSKI;
                }
                administrator.Email = line.Split(' ')[5];
                administrator.JMBG = line.Split(' ')[6];
                administrator.Telefon = line.Split(' ')[7];
                administrator.Uloga = EUloga.DISPECER;

                administratori.Add(administrator.KorisnickoIme, administrator);
            }

            HttpContext.Current.Session["Administratori"] = administratori;
            reader.Close();


            Dictionary<string, Vozac> vozaci = new Dictionary<string, Vozac>();
            if (!File.Exists(path + "packages/Vozaci.txt"))
            {
                File.Create(path + "packages/Vozaci.txt");
            }

            StreamReader reader2 = new StreamReader(path + "packages/Vozaci.txt");
            while ((line = reader2.ReadLine()) != null)
            {
                Vozac vozac = new Vozac();
                vozac.KorisnickoIme = line.Split(' ')[0];
                vozac.Lozinka = line.Split(' ')[1];
                vozac.Ime = line.Split(' ')[2];
                vozac.Prezime = line.Split(' ')[3];
                if (line.Split(' ')[4].Equals("MUSKI"))
                {
                    vozac.Pol = EPol.MUSKI;
                }
                else
                {
                    vozac.Pol = EPol.ZENSKI;
                }
                vozac.Email = line.Split(' ')[5];
                vozac.JMBG = line.Split(' ')[6];
                vozac.Telefon = line.Split(' ')[7];
                vozac.Uloga = EUloga.VOZAC;

                vozac.AutoVozac = new Automobil();
                vozac.AutoVozac.BrojVozila = Int32.Parse(line.Split(' ')[8]);
                vozac.AutoVozac.Godiste = Int32.Parse(line.Split(' ')[9]);
                vozac.AutoVozac.RegistarskaOznaka = line.Split(' ')[10];
                if (line.Split(' ')[11].ToLower().Equals("putnicki"))
                {
                    vozac.AutoVozac.TipAutomobila = ETipAutomobila.PUTNICKI;
                }
                else
                {
                    vozac.AutoVozac.TipAutomobila = ETipAutomobila.KOMBI;
                }

                vozac.LokVozac = new Lokacija();
                vozac.LokVozac.XCoord = double.Parse(line.Split(' ')[12]);
                vozac.LokVozac.YCoord = double.Parse(line.Split(' ')[13]);
                vozac.LokVozac.AdresaLok = new Adresa();
                vozac.LokVozac.AdresaLok.Ulica = line.Split(' ')[14];
                vozac.LokVozac.AdresaLok.Broj = Int32.Parse(line.Split(' ')[15]);
                vozac.LokVozac.AdresaLok.Mesto = line.Split(' ')[16];
                vozac.LokVozac.AdresaLok.PozivniBroj = Int32.Parse(line.Split(' ')[17]);
                vozac.Zauzet = false;
                vozaci.Add(vozac.KorisnickoIme, vozac);
            }

            HttpContext.Current.Session["Vozaci"] = vozaci;
            reader2.Close();

            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();
            if (!File.Exists(path + "packages/Korisnici.txt"))
            {
                File.Create(path + "packages/Korisnici.txt");
            }
            StreamReader reader3 = new System.IO.StreamReader(path + "packages/Korisnici.txt");
            while ((line = reader3.ReadLine()) != null)
            {
                Korisnik korisnik = new Korisnik();
                korisnik.KorisnickoIme = line.Split(' ')[0];
                korisnik.Lozinka = line.Split(' ')[1];
                korisnik.Ime = line.Split(' ')[2];
                korisnik.Prezime = line.Split(' ')[3];
                if (line.Split(' ')[4].Equals("MUSKI"))
                {
                    korisnik.Pol = EPol.MUSKI;
                }
                else
                {
                    korisnik.Pol = EPol.ZENSKI;
                }
                korisnik.Email = line.Split(' ')[5];
                korisnik.JMBG = line.Split(' ')[6];
                korisnik.Telefon = line.Split(' ')[7];
                korisnik.Uloga = EUloga.MUSTERIJA;

                korisnici.Add(korisnik.KorisnickoIme, korisnik);
            }

            reader3.Close();
            HttpContext.Current.Session["RegistrovaniKorisnici"] = korisnici;

            HttpContext.Current.Session["Voznje"] = new Dictionary<string, Voznja>();

            Dictionary<string, Voznja> voznje = new Dictionary<string, Voznja>();
            if (!File.Exists(path + "packages/Voznje.txt"))
            {
                File.Create(path + "packages/Voznje.txt");
            }
            StreamReader reader4 = new StreamReader(path + "packages/Voznje.txt");
            while((line = reader4.ReadLine()) != null)
            {
                Voznja v = new Voznja();
                v.LokacijaDolaskaTaxi = new Lokacija();
                v.LokacijaDolaskaTaxi.XCoord = double.Parse(line.Split(' ')[0]);
                v.LokacijaDolaskaTaxi.YCoord = double.Parse(line.Split(' ')[1]);
                v.LokacijaDolaskaTaxi.AdresaLok = new Adresa();
                v.LokacijaDolaskaTaxi.AdresaLok.Ulica = line.Split(' ')[2];
                v.LokacijaDolaskaTaxi.AdresaLok.Broj = Int32.Parse(line.Split(' ')[3]);
                v.LokacijaDolaskaTaxi.AdresaLok.Mesto = line.Split(' ')[4];
                v.LokacijaDolaskaTaxi.AdresaLok.PozivniBroj = Int32.Parse(line.Split(' ')[5]);
                v.DatumVremePorudzbine = DateTime.Parse(line.Split(' ')[6]);
                if (line.Split(' ')[7].ToLower().Equals("kreirana"))
                {
                    v.StatusVoznje = Models.EStatus.KREIRANA;
                    
                }
                else if (line.Split(' ')[7].ToLower().Equals("formirana"))
                {
                    v.StatusVoznje = Models.EStatus.FORMIRANA;
                }
                else if (line.Split(' ')[7].ToLower().Equals("obradjena"))
                {
                    v.StatusVoznje = Models.EStatus.OBRADJENA;
                }
                else if(line.Split(' ')[7].ToLower().Equals("prihvacena"))
                {
                    v.StatusVoznje = Models.EStatus.PRIHVACENA;
                }
                else if(line.Split(' ')[7].ToLower().Equals("otkazana"))
                {
                    v.StatusVoznje = Models.EStatus.OTKAZANA;
                }
                else if(line.Split(' ')[7].ToLower().Equals("neuspesna"))
                {
                    v.StatusVoznje = Models.EStatus.NEUSPESNA;
                }
                else if(line.Split(' ')[7].ToLower().Equals("uspesna"))
                {
                    v.StatusVoznje = Models.EStatus.USPESNA;
                }
                v.Musterija = new Korisnik();
                v.Musterija.KorisnickoIme = line.Split(' ')[8];
                v.Musterija.Lozinka = line.Split(' ')[9];
                v.Musterija.Ime = line.Split(' ')[10];
                v.Musterija.Prezime = line.Split(' ')[11];
                if(line.Split(' ')[12].ToLower().Equals("m"))
                {
                    v.Musterija.Pol = EPol.MUSKI;
                }
                else
                {
                    v.Musterija.Pol = EPol.ZENSKI;
                }
                v.Musterija.Email = line.Split(' ')[13];
                v.Musterija.JMBG = line.Split(' ')[14];
                v.Musterija.Telefon = line.Split(' ')[15];
                v.Musterija.Uloga = EUloga.MUSTERIJA;
                v.Vozac = new Vozac();
               
                voznje.Add(v.DatumVremePorudzbine.ToString(), v);

                reader4.Close();
                HttpContext.Current.Session["Voznje"] = voznje;
            }
        }
    }
}
