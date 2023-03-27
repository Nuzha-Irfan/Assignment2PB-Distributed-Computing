using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using Website.Models;
using RestSharp;
using Newtonsoft.Json;
namespace Website.Controllers
{
    public class Clients : Controller
    {
        Job job2 = new Job();
        public IActionResult Index()
        {
            
            return View();
        }


        public IActionResult GetID()
        {
            Job job = new Job();

            RestClient restClient = new RestClient("https://localhost:44304/");


            RestRequest restRequest = new RestRequest("api/Jobs/");


            RestResponse resp = restClient.Get(restRequest);


            List<Job> Details = JsonConvert.DeserializeObject<List<Job>>(resp.Content);


            foreach(Job job1 in Details)
            {
                if (job1.Id == Details.Count -1)
                {
                    job = job1;
                    
                }
                
            }
            return Ok(job);
          
        }
    }
}
