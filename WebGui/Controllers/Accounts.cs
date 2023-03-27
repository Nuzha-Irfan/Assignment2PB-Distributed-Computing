using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using WebGui.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebGui.Controllers
{
    public class Accounts : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Search(string searchtext)
        {

            RestClient restClient = new RestClient("https://localhost:44376/");
            RestRequest restRequest = new RestRequest("api/Search/{id}", Method.Get);
            restRequest.AddUrlSegment("id", searchtext);
            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse.Content);
        }
        [HttpPost]
        public IActionResult Insert(Account account)
        {
            RestClient restClient = new RestClient("https://localhost:44376/");
            RestRequest restRequest = new RestRequest("api/Accounts", Method.Post);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(account));
            RestResponse restResponse = restClient.Execute(restRequest);

            Account returnDetails = JsonConvert.DeserializeObject<Account>(restResponse.Content);
            if (returnDetails != null)
            {
                return Ok(returnDetails);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            RestClient restClient = new RestClient("https://localhost:44376/");

            RestRequest restRequest = new RestRequest("api/Accounts/" + id);


            RestResponse resp = restClient.Delete(restRequest);


            Account AccDetails = JsonConvert.DeserializeObject<Account>(resp.Content);
            if (AccDetails != null)
            {
                return Ok(AccDetails);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Update (int id, Account account)
        {
            RestClient restClient = new RestClient("https://localhost:44376/");
            RestRequest restRequest = new RestRequest("api/Accounts/{id}", Method.Put);
            restRequest.AddUrlSegment("id", id);
            restRequest.AddJsonBody(JsonConvert.SerializeObject(account));
            RestResponse restResponse = restClient.Execute(restRequest);

            return Ok(restResponse);
        }
        [HttpPost]
        public IActionResult Generate()
        {
            RestClient restClient = new RestClient("https://localhost:44376/");
            RestRequest restRequest = new RestRequest("api/DatabaseGenarate/", Method.Post);


            RestResponse restResponse = restClient.Execute(restRequest);
            return Ok(restResponse);
        }
        [HttpGet]
        public IActionResult SearchbyID(int id)
        {
            RestClient restClient = new RestClient("https://localhost:44376/");

            
            RestRequest restRequest = new RestRequest("api/Accounts/{id}", Method.Get);
            restRequest.AddUrlSegment("id", id);
            RestResponse restResponse = restClient.Execute(restRequest);

            return Ok(restResponse.Content);


        }
        
        
        [HttpPost]
        public  IActionResult UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        file.CopyToAsync(fileStream);
                    }
                    return Ok(path);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }
    }
}
