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
    public class jobsDescriptionsController : ApiController
    {
        private JobsEntities3 db = new JobsEntities3();

        // GET: api/jobsDescriptions
        public IQueryable<jobsDescription> GetjobsDescriptions()
        {
            return db.jobsDescriptions;
        }

        // GET: api/jobsDescriptions/5
        [ResponseType(typeof(jobsDescription))]
        public IHttpActionResult GetjobsDescription(int id)
        {
            jobsDescription jobsDescription = db.jobsDescriptions.Find(id);
            if (jobsDescription == null)
            {
                return NotFound();
            }

            return Ok(jobsDescription);
        }

        // PUT: api/jobsDescriptions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutjobsDescription(int id, jobsDescription jobsDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobsDescription.Id)
            {
                return BadRequest();
            }

            db.Entry(jobsDescription).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!jobsDescriptionExists(id))
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

        // POST: api/jobsDescriptions
        [ResponseType(typeof(jobsDescription))]
        public IHttpActionResult PostjobsDescription(jobsDescription jobsDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.jobsDescriptions.Add(jobsDescription);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jobsDescription.Id }, jobsDescription);
        }

        // DELETE: api/jobsDescriptions/5
        [ResponseType(typeof(jobsDescription))]
        public IHttpActionResult DeletejobsDescription(int id)
        {
            jobsDescription jobsDescription = db.jobsDescriptions.Find(id);
            if (jobsDescription == null)
            {
                return NotFound();
            }

            db.jobsDescriptions.Remove(jobsDescription);
            db.SaveChanges();

            return Ok(jobsDescription);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool jobsDescriptionExists(int id)
        {
            return db.jobsDescriptions.Count(e => e.Id == id) > 0;
        }
    }
}