using Microsoft.AspNetCore.Mvc;

namespace NZWalk.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async IActionResult Index()
        {
            //Get All Regions from Web API
            var client = httpClientFactory.CreateClient();

            await client.GetAsync("https://localhost:7207");

            return View();
        }
    }
}
