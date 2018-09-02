using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models.Entiteti
{
    public class KorisnikEntitet : DbContext
    {
        public DbSet<Korisnik> Korisnici { get; set; }
        public KorisnikEntitet() : base("name=TaxiTablesDBConnectionString") { }

        public System.Data.Entity.DbSet<WebAPI_Taxi.Models.Automobil> Automobils { get; set; }
    }
}