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
    public class PLATFORMsController : ApiController
    {
        private Entities db = new Entities();

        // GET: api/PLATFORMs
        public IQueryable<PLATFORM> GetPLATFORMs()
        {
            return db.PLATFORMs;
        }

        // GET: api/PLATFORMs/5
        [ResponseType(typeof(PLATFORM))]
        public IHttpActionResult GetPLATFORM(decimal id)
        {
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            if (pLATFORM == null)
            {
                return NotFound();
            }

            return Ok(pLATFORM);
        }

        // PUT: api/PLATFORMs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPLATFORM(decimal id, PLATFORM pLATFORM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pLATFORM.PLATFORM_ID)
            {
                return BadRequest();
            }

            db.Entry(pLATFORM).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PLATFORMExists(id))
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

        // POST: api/PLATFORMs
        [ResponseType(typeof(PLATFORM))]
        public IHttpActionResult PostPLATFORM(PLATFORM pLATFORM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PLATFORMs.Add(pLATFORM);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PLATFORMExists(pLATFORM.PLATFORM_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = pLATFORM.PLATFORM_ID }, pLATFORM);
        }

        // DELETE: api/PLATFORMs/5
        [ResponseType(typeof(PLATFORM))]
        public IHttpActionResult DeletePLATFORM(decimal id)
        {
            PLATFORM pLATFORM = db.PLATFORMs.Find(id);
            if (pLATFORM == null)
            {
                return NotFound();
            }

            db.PLATFORMs.Remove(pLATFORM);
            db.SaveChanges();

            return Ok(pLATFORM);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PLATFORMExists(decimal id)
        {
            return db.PLATFORMs.Count(e => e.PLATFORM_ID == id) > 0;
        }
    }
}