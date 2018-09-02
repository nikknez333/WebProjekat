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

            Dictionary<string, Dispecer> administratori = new Dictionary<string, Dispecer>();
            string line;

            if(!File.Exists(path + "packages/Administratori.txt"))
            {
                File.Create(path + "packages/Administratori.txt");
                File.AppendAllText(path + "packages/Administratori.txt",
                    "knez7" + Environment.NewLine +
                    "edctgbuj12" + Environment.NewLine +
                    "Nebojsa" + Environment.NewLine +
                    "Knezevic" + Environment.NewLine +
                    "M" + Environment.NewLine +
                    "nebojsaknezz@gmail.com" + Environment.NewLine +
                    "2403996810113" + Environment.NewLine +
                    "0601348766" + Environment.NewLine +
                    "ivana771" + Environment.NewLine +
                    "ivAna2536" + Environment.NewLine +
                    "Ivana" + Environment.NewLine +
                    "Ivanovic" + Environment.NewLine +
                    "Z" + Environment.NewLine +
                    "ivanaI771@gmail.com" + Environment.NewLine +
                    "1208992456334" + Environment.NewLine +
                    "0643346687" + Environment.NewLine +
                    "marko655" + Environment.NewLine +
                    "markoMark221" + Environment.NewLine +
                    "Marko" + Environment.NewLine +
                    "Markovic" + Environment.NewLine +
                    "M" + Environment.NewLine +
                    "markoMar226@yahoo.com" + Environment.NewLine +
                    "1812997214775" + Environment.NewLine +
                    "0622234986"); 
            }

            StreamReader reader = new StreamReader(path + "packages/Administratori.txt");
            while((line= reader.ReadLine()) != null)
            {
                Dispecer administrator = new Dispecer();

                administrator.KorisnickoIme = line.Split('\n')[0];
                administrator.Lozinka = line.Split('\n')[1];
                administrator.Ime = line.Split('\n')[2];
                administrator.Prezime = line.Split('\n')[3];
                if(line.Split('\n')[4].ToLower().Equals("m"))
                {
                    administrator.Pol = EPol.MUSKI;
                }
                else
                {
                    administrator.Pol = EPol.ZENSKI;
                }
                administrator.Email = line.Split('\n')[5];
                administrator.JMBG = line.Split('\n')[6];
                administrator.Telefon = line.Split('\n')[7];
                administrator.Uloga = EUloga.DISPECER;

                administratori.Add(administrator.KorisnickoIme, administrator);
            }

            HttpContext.Current.Application["Administratori"] = administratori;
            reader.Close();


            Dictionary<string, Vozac> vozaci = new Dictionary<string, Vozac>();
            if(!File.Exists(path + "packages/Vozaci.txt"))
            {
                File.Create(path + "packages/Vozaci.txt");
            }

            StreamReader reader2 = new StreamReader(path + "packages/Vozaci.txt");
            while ((line = reader2.ReadLine()) != null)
            {
                Vozac vozac = new Vozac();
                vozac.KorisnickoIme = line.Split('\n')[0];
                vozac.Lozinka = line.Split('\n')[1];
                vozac.Ime = line.Split('\n')[2];
                vozac.Prezime = line.Split('\n')[3];
                if (line.Split('\n')[4].ToLower().Equals("m"))
                {
                    vozac.Pol = EPol.MUSKI;
                }
                else
                {
                    vozac.Pol = EPol.ZENSKI;
                }
                vozac.Email = line.Split('\n')[5];
                vozac.JMBG = line.Split('\n')[6];
                vozac.Telefon = line.Split('\n')[7];
                vozac.Uloga = EUloga.VOZAC;

                vozac.AutoVozac = new Automobil();
                vozac.AutoVozac.BrojVozila = Int32.Parse(line.Split('\n')[8]);
                vozac.AutoVozac.Godiste = Int32.Parse(line.Split('\n')[9]);
                vozac.AutoVozac.RegistarskaOznaka = line.Split('\n')[10];
                if (line.Split('\n')[11].ToLower().Equals("putnicki"))
                {
                    vozac.AutoVozac.TipAutomobila = ETipAutomobila.PUTNICKI;
                }
                else
                {
                    vozac.AutoVozac.TipAutomobila = ETipAutomobila.KOMBI;
                }

                vozac.LokVozac = new Lokacija();
                vozac.LokVozac.XCoord = double.Parse(line.Split('\n')[12]);
                vozac.LokVozac.YCoord = double.Parse(line.Split('\n')[13]);
                vozac.LokVozac.AdresaLok = new Adresa();
                vozac.LokVozac.AdresaLok.Ulica = line.Split('\n')[14];
                vozac.LokVozac.AdresaLok.Broj = Int32.Parse(line.Split('\n')[15]);
                vozac.LokVozac.AdresaLok.Mesto = line.Split('\n')[16];
                vozac.LokVozac.AdresaLok.PozivniBroj = Int32.Parse(line.Split('\n')[17]);
                vozac.Zauzet = false;
                vozaci.Add(vozac.KorisnickoIme, vozac);                
            }

            HttpContext.Current.Application["Vozaci"] = vozaci;
            reader2.Close();

            Dictionary<string, Musterija> musterije = new Dictionary<string, Musterija>();
            if (!File.Exists(path + "packages/Korisnici.txt"))
            {
                File.Create(path + "packages/Korisnici.txt");
            }
            StreamReader reader3 = new System.IO.StreamReader(path + "packages/Korisnici.txt");
            while ((line = reader3.ReadLine()) != null)
            {
                Musterija musterija = new Musterija();
                musterija.KorisnickoIme = line.Split('\n')[0];
                musterija.Lozinka = line.Split('\n')[1];
                musterija.Ime = line.Split('\n')[2];
                musterija.Prezime = line.Split('\n')[3];
                if (line.Split('\n')[4].Equals("MUSKI"))
                {
                    musterija.Pol = EPol.MUSKI;
                }
                else
                {
                    musterija.Pol = EPol.ZENSKI;
                }
                musterija.Email = line.Split('\n')[5];
                musterija.JMBG = line.Split('\n')[6];
                musterija.Telefon = line.Split('\n')[7]; 
                musterija.Uloga = EUloga.MUSTERIJA;
                
                musterije.Add(musterija.KorisnickoIme, musterija);
            }

            reader3.Close();
            HttpContext.Current.Application["RegistrovaniKorisnici"] = musterije;

            HttpContext.Current.Application["Voznje"] = new Dictionary<string, Voznja>();

        }
    }
}
