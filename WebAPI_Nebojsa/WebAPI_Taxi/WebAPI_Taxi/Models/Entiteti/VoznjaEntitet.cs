using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models.Entiteti
{
    public class VoznjaEntitet : DbContext
    {
        public DbSet<Voznja> Voznjas { get; set; }
        public VoznjaEntitet() : base("name=TaxiTablesDBConnectionString") { }

        public DbSet<Korisnik> Korisniks { get; set; }

        public DbSet<Komentar> Komentars { get; set; }
    }
}