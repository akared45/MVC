using Long.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Bogus;

namespace Long.Controllers
{
    public class HomeController : Controller
    {
        public static List<Category> categories = new List<Category>();
        public static List<Product> products = new List<Product>();

        static HomeController()
        {
            GenerateData();
        }
        public static void GenerateData()
        {
            if (categories.Count == 0)
            {
                categories = new List<Category>
                {
                    new Category { Id = 1, Name = "Điện thoại" },
                    new Category { Id = 2, Name = "Laptop" },
                    new Category { Id = 3, Name = "Tablet" },
                    new Category { Id = 4, Name = "Phụ kiện" }
                };

                var faker = new Faker<Product>()
                    .RuleFor(p => p.Id, f => f.IndexFaker + 1)
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price(1000000, 50000000, 0)))
                    .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(p => p.ImageUrl, f => f.Image.PicsumUrl())
                    .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id);

                products = faker.Generate(100);

                foreach (var cat in categories)
                {
                    cat.Products = products.Where(p => p.CategoryId == cat.Id).ToList();
                }
            }
        }
    public ActionResult Index()
        {
            var groupedProducts = products
                .GroupBy(p => categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name ?? "Unknown")
                .ToDictionary(g => g.Key, g => g.ToList());

            return View(groupedProducts);
        }
    }
}