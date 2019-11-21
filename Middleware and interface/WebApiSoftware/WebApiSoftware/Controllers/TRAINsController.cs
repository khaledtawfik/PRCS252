using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSoftware.Models;

namespace WebApiSoftware.Controllers
{
    [KnownType(typeof(ICollection<JOURNEY>))]
    [KnownType(typeof(JOURNEY))]
    [DataContract]
    public class TRAINsController : ApiController
    {
        private Entities db = new Entities();
       
        // GET: api/TRAINs
        //public string GetTRAINs()
        public IQueryable<TRAIN> GetTRAINs()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.TRAINs;
            TRAIN train = db.TRAINs.Where<TRAIN>(e => e.TRAIN_ID == 1).FirstOrDefault();
            //return train.MODEL;
            //return db.TRAINs.Count(e => e.TRAIN_ID == 4) > 0;
        }

        // GET: api/TRAINs/5
        [ResponseType(typeof(TRAIN))]
        public IHttpActionResult GetTRAIN(decimal id)
        {
            TRAIN tRAIN = db.TRAINs.Find(id);
            if (tRAIN == null)
            {
                return NotFound();
            }

            return Ok(tRAIN);
        }

        // PUT: api/TRAINs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTRAIN(decimal id, TRAIN tRAIN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tRAIN.TRAIN_ID)
            {
                return BadRequest();
            }

            db.Entry(tRAIN).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TRAINExists(id))
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

        // POST: api/TRAINs
        [ResponseType(typeof(TRAIN))]
        public IHttpActionResult PostTRAIN(TRAIN tRAIN)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TRAINs.Add(tRAIN);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TRAINExists(tRAIN.TRAIN_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tRAIN.TRAIN_ID }, tRAIN);
        }

        // DELETE: api/TRAINs/5
        [ResponseType(typeof(TRAIN))]
        public IHttpActionResult DeleteTRAIN(decimal id)
        {
            TRAIN tRAIN = db.TRAINs.Find(id);
            if (tRAIN == null)
            {
                return NotFound();
            }

            db.TRAINs.Remove(tRAIN);
            db.SaveChanges();

            return Ok(tRAIN);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TRAINExists(decimal id)
        {
            return db.TRAINs.Count(e => e.TRAIN_ID == id) > 0;
        }
    }
}