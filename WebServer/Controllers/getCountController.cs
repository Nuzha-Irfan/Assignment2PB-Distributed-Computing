using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers
{
    [RoutePrefix("api/count")]
    public class getCountController : ApiController
    {
        private JobsEntities3 db = new JobsEntities3();

        [Route("{clientName}")]
        [HttpGet]
        public jobsCount GetJobsCount(string name)
        {
            List<jobsCount> jobs = new List<jobsCount>();
            List<jobsCount> jobs1 = new List<jobsCount>();

            jobs = db.jobsCounts.ToList();
            jobsCount clientJobs = new jobsCount();
            foreach (jobsCount job in jobs)
            {
                if (String.Equals(job.clientName, name))
                {
                    clientJobs = job;
                    return clientJobs;
                }
            }
            return null;
        }
    }
}
