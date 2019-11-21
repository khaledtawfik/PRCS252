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
    public class JOURNEYsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/JOURNEYs
        public IQueryable<JOURNEY> GetJOURNEYs()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.JOURNEYs;
        }

        // GET: api/JOURNEYs/5
        [ResponseType(typeof(JOURNEY))]
        public IQueryable<JOURNEY> GetJOURNEY(decimal departure_id, decimal destination_id, DateTime date)
        {
            db.Configuration.ProxyCreationEnabled = false;

            IQueryable<JOURNEY> jOURNEYs = db.JOURNEYs.Where<JOURNEY>(e => e.DEPARTURE_STATION_ID == departure_id &
                                                                e.DESTINATION_STATION_ID == destination_id &
                                                                e.JOURNEY_DATE.Value.Year == date.Year &
                                                                e.JOURNEY_DATE.Value.Month == date.Month &
                                                                e.JOURNEY_DATE.Value.Day == date.Day);
                                                                //e.JOURNEY_DATE >= date.Date);

            //JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEYs == null)
            {
                return null;
            }
            return jOURNEYs;
        }

        /*
        // GET: api/JOURNEYs/5
        [ResponseType(typeof(JOURNEY))]
        public IHttpActionResult GetJOURNEY(decimal departure_id, decimal destination_id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            JOURNEY jOURNEY = db.JOURNEYs.Where<JOURNEY>(e => e.DEPARTURE_STATION_ID == departure_id &
                                                                e.DESTINATION_STATION_ID == destination_id);
            //JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEY == null)
            {
                return NotFound();
            }
            return Ok(jOURNEY);
        }*/





        // PUT: api/JOURNEYs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJOURNEY(decimal id, JOURNEY jOURNEY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jOURNEY.JOURNEY_ID)
            {
                return BadRequest();
            }

            db.Entry(jOURNEY).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JOURNEYExists(id))
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

        // POST: api/JOURNEYs
        [ResponseType(typeof(JOURNEY))]
        public IHttpActionResult PostJOURNEY(JOURNEY jOURNEY)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JOURNEYs.Add(jOURNEY);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (JOURNEYExists(jOURNEY.JOURNEY_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = jOURNEY.JOURNEY_ID }, jOURNEY);
        }

        // DELETE: api/JOURNEYs/5
        [ResponseType(typeof(JOURNEY))]
        public IHttpActionResult DeleteJOURNEY(decimal id)
        {
            JOURNEY jOURNEY = db.JOURNEYs.Find(id);
            if (jOURNEY == null)
            {
                return NotFound();
            }

            db.JOURNEYs.Remove(jOURNEY);
            db.SaveChanges();

            return Ok(jOURNEY);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JOURNEYExists(decimal id)
        {
            return db.JOURNEYs.Count(e => e.JOURNEY_ID == id) > 0;
        }
    }
}