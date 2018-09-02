using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI_Taxi.Models;
using WebAPI_Taxi.Models.Entiteti;

namespace WebAPI_Taxi.Controllers
{
    public class LokacijasController : ApiController
    {
        private LokacijaEntitet db = new LokacijaEntitet();

        // GET: api/Lokacijas
        public IQueryable<Lokacija> GetLokacije()
        {
            return db.Lokacije;
        }

        // GET: api/Lokacijas/5
        [ResponseType(typeof(Lokacija))]
        public IHttpActionResult GetLokacija(string id)
        {
            Lokacija lokacija = db.Lokacije.Find(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            return Ok(lokacija);
        }

        // PUT: api/Lokacijas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLokacija(string id, Lokacija lokacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lokacija.LokacijaKey)
            {
                return BadRequest();
            }

            db.Entry(lokacija).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LokacijaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Lokacijas
        [ResponseType(typeof(Lokacija))]
        public IHttpActionResult PostLokacija(Lokacija lokacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lokacije.Add(lokacija);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LokacijaExists(lokacija.LokacijaKey))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = lokacija.LokacijaKey }, lokacija);
        }

        // DELETE: api/Lokacijas/5
        [ResponseType(typeof(Lokacija))]
        public IHttpActionResult DeleteLokacija(string id)
        {
            Lokacija lokacija = db.Lokacije.Find(id);
            if (lokacija == null)
            {
                return NotFound();
            }

            db.Lokacije.Remove(lokacija);
            db.SaveChanges();

            return Ok(lokacija);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LokacijaExists(string id)
        {
            return db.Lokacije.Count(e => e.LokacijaKey == id) > 0;
        }
    }
}