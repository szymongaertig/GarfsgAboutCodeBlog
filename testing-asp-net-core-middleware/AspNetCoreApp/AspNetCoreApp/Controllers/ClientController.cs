using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp.Controllers
{
    public class ClientController : Controller
    {
        [HttpGet]
        [Route("products/{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok($"Product: ${id} data");
        }
    }
}