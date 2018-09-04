﻿using System;
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
                if (line.Split(' ')[4].ToLower().Equals("m"))
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
        }
    }
}
