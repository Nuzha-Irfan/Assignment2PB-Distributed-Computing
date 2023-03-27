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
using WebServer.Models;

namespace WebServer.Controllers
{
    public class jobsCountsController : ApiController
    {
        private JobsEntities3 db = new JobsEntities3();

        // GET: api/jobsCounts
        public IQueryable<jobsCount> GetjobsCounts()
        {
            return db.jobsCounts;
        }

        // GET: api/jobsCounts/5
        [ResponseType(typeof(jobsCount))]
        public IHttpActionResult GetjobsCount(int id)
        {
            jobsCount jobsCount = db.jobsCounts.Find(id);
            if (jobsCount == null)
            {
                return NotFound();
            }

            return Ok(jobsCount);
        }

        // PUT: api/jobsCounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutjobsCount(int id, jobsCount jobsCount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobsCount.Id)
            {
                return BadRequest();
            }

            db.Entry(jobsCount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!jobsCountExists(id))
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

        // POST: api/jobsCounts
        [ResponseType(typeof(jobsCount))]
        public IHttpActionResult PostjobsCount(jobsCount jobsCount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobsCounts.Add(jobsCount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobsCount.Id }, jobsCount);
        }

        // DELETE: api/jobsCounts/5
        [ResponseType(typeof(jobsCount))]
        public IHttpActionResult DeletejobsCount(int id)
        {
            jobsCount jobsCount = db.jobsCounts.Find(id);
            if (jobsCount == null)
            {
                return NotFound();
            }

            db.jobsCounts.Remove(jobsCount);
            db.SaveChanges();

            return Ok(jobsCount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool jobsCountExists(int id)
        {
            return db.jobsCounts.Count(e => e.Id == id) > 0;
        }
    }
}