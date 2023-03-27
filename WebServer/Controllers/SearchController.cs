using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers
{
    [RoutePrefix("api/search")]
    public class SearchController : ApiController
    {
        private JobsEntities3 db = new JobsEntities3();

        [Route("{clientName}")]
        [HttpGet]
        public List<jobsDescription> GetJobsForClient(string clientName)
        {
            List<jobsDescription> jobs = new List<jobsDescription>();
            List<jobsDescription> jobs1 = new List<jobsDescription>();

            jobs = db.jobsDescriptions.ToList();
            jobsDescription clientJobs =  new jobsDescription();
            foreach(jobsDescription job in jobs)
            {
                if(String.Equals(job.clientName , clientName))
                {
                    clientJobs = job;
                    jobs1.Add(clientJobs);
                }
            }
            return jobs1;
        }

       
    }
}
