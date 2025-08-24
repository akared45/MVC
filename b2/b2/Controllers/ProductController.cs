using Microsoft.AspNetCore.Mvc;
using b2.Models;
using System.Collections.Generic;

namespace b2.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "iPhone 15 Pro", Price = 27990000 },
            new Product { Id = 2, Name = "Samsung Galaxy S23", Price = 21990000 },
            new Product { Id = 3, Name = "MacBook Air M2", Price = 32990000 },
            new Product { Id = 4, Name = "Dell XPS 13", Price = 28990000 },
            new Product { Id = 5, Name = "Sony WH-1000XM5", Price = 7990000 }
        };

        public IActionResult Index()
        {
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = products.Find(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
