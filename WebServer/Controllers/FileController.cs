using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebServer.Models;

namespace WebServer.Controllers
{
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private JobsEntities3 db = new JobsEntities3();

        [Route("{fileName}")]
        [HttpGet]
        public jobsDescription GetPythonScript(string fileName)
        {
            List<jobsDescription> jobs = new List<jobsDescription>();
            List<jobsDescription> jobs1 = new List<jobsDescription>();

            /*jobs = db.jobsDescriptions.ToList();
            jobsDescription clientJobs = new jobsDescription();
            foreach (jobsDescription job in jobs)
            {
                if (String.Equals(job.jobDes, fileName,StringComparison.OrdinalIgnoreCase))
                {
                    clientJobs = job;
                    return clientJobs.Id;
                }
            }
            return 0;*/

            jobs = db.jobsDescriptions.ToList();
            jobsDescription clientJobs = new jobsDescription();
            foreach (jobsDescription job in jobs)
            {
                if (String.Equals(job.jobDes, fileName))
                {
                    clientJobs = job;
                    return clientJobs;
                }
            }
            return null;
        }
    }
}
