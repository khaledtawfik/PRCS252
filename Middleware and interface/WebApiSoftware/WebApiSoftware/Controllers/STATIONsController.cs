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
using WebApiSoftware.Models;

namespace WebApiSoftware.Controllers
{
    public class STATIONsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/STATIONs
        public IQueryable<STATION> GetSTATIONs()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.STATIONs;
        }

        // GET: api/STATIONs/5
        [ResponseType(typeof(STATION))]
        public IHttpActionResult GetSTATION(string id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            STATION sTATION = db.STATIONs.Where<STATION>(e => e.CITY == id).FirstOrDefault();
            if (sTATION == null)
            {
                return NotFound();
            }

            return Ok(sTATION);
        }

        // PUT: api/STATIONs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSTATION(decimal id, STATION sTATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sTATION.STATION_ID)
            {
                return BadRequest();
            }

            db.Entry(sTATION).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!STATIONExists(id))
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

        // POST: api/STATIONs
        [ResponseType(typeof(STATION))]
        public IHttpActionResult PostSTATION(STATION sTATION)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.STATIONs.Add(sTATION);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (STATIONExists(sTATION.STATION_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = sTATION.STATION_ID }, sTATION);
        }

        // DELETE: api/STATIONs/5
        [ResponseType(typeof(STATION))]
        public IHttpActionResult DeleteSTATION(decimal id)
        {
            STATION sTATION = db.STATIONs.Find(id);
            if (sTATION == null)
            {
                return NotFound();
            }

            db.STATIONs.Remove(sTATION);
            db.SaveChanges();

            return Ok(sTATION);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool STATIONExists(decimal id)
        {
            return db.STATIONs.Count(e => e.STATION_ID == id) > 0;
        }
    }
}