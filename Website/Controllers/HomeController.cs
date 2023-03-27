using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
   
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            
            RestClient restClient = new RestClient("https://localhost:44304/");


            RestRequest restRequest = new RestRequest("api/Jobs/");


            RestResponse resp = restClient.Get(restRequest);


            List<Job> Details = JsonConvert.DeserializeObject<List<Job>>(resp.Content);
            return View(Details);
        }

    }
}
