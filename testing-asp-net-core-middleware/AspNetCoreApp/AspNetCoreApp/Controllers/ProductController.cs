using AspNetCoreApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApp.Controllers
{
    [Route("api")]
    public class ProductController : Controller
    {
        private List<Product> _products;
        public ProductController()
        {
            _products = new List<Product>()
            {
                new Product(1,"Socks",10,2,"Very secret comment"),
                new Product(2,"Shirt",23,5,"Very secret comment"),
                new Product(3,"Other socks",10,2,"Very secret comment"),
                new Product(4,"Erring PC",1231,499,"Very secret comment")
            };
        }

        [HttpGet]
        [Route("products")]
        public IActionResult GetProducts()
        {
            return Ok(_products.Select(x => new
            {
                name = x.Name,
                price = x.Price
            }));
        }

        [HttpGet]
        [Route("products/{id}")]
        public IActionResult GetProduct([FromRoute]int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}