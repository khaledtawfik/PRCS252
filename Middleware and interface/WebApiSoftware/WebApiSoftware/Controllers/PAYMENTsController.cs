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
    public class PAYMENTsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PAYMENTs
        public IQueryable<PAYMENT> GetPAYMENTs()
        {
            return db.PAYMENTs;
        }

        // GET: api/PAYMENTs/5
        [ResponseType(typeof(PAYMENT))]
        public IHttpActionResult GetPAYMENT(decimal id)
        {
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return NotFound();
            }

            return Ok(pAYMENT);
        }

        // PUT: api/PAYMENTs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPAYMENT(decimal id, PAYMENT pAYMENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pAYMENT.PAYMENT_ID)
            {
                return BadRequest();
            }

            db.Entry(pAYMENT).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PAYMENTExists(id))
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

        // POST: api/PAYMENTs
        [ResponseType(typeof(PAYMENT))]
        public IHttpActionResult PostPAYMENT(PAYMENT pAYMENT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PAYMENTs.Add(pAYMENT);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PAYMENTExists(pAYMENT.PAYMENT_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pAYMENT.PAYMENT_ID }, pAYMENT);
        }

        // DELETE: api/PAYMENTs/5
        [ResponseType(typeof(PAYMENT))]
        public IHttpActionResult DeletePAYMENT(decimal id)
        {
            PAYMENT pAYMENT = db.PAYMENTs.Find(id);
            if (pAYMENT == null)
            {
                return NotFound();
            }

            db.PAYMENTs.Remove(pAYMENT);
            db.SaveChanges();

            return Ok(pAYMENT);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PAYMENTExists(decimal id)
        {
            return db.PAYMENTs.Count(e => e.PAYMENT_ID == id) > 0;
        }
    }
}