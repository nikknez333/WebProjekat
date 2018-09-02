using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebAPI_Taxi.Models.Entiteti
{
    public class LokacijaEntitet : DbContext
    {
        public DbSet<Lokacija> Lokacije { get; set; }
        public LokacijaEntitet() : base("name=TaxiTablesDBConnectionString") { }
    }
}