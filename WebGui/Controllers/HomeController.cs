using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGui.Models;

namespace WebGui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Home";
            RestClient restClient = new RestClient("https://localhost:44376/");

            
            RestRequest restRequest = new RestRequest("api/Accounts/");


            RestResponse resp = restClient.Get(restRequest);


            List <Account> AccDetails = JsonConvert.DeserializeObject<List<Account>>(resp.Content);
            return View(AccDetails);
        }
    }
}
