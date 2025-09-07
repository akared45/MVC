using Long.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Long.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ListByCategory(string categoryName)
        {
            var category = HomeController.categories.FirstOrDefault(c => c.Name == categoryName);
            if (category == null)
            {
                return NotFound();
            }

            var categoryProducts = HomeController.products.Where(p => p.CategoryId == category.Id).ToList();
            ViewBag.CategoryName = categoryName;
            return View(categoryProducts);
        }

        public ActionResult Detail(int id)
        {
            HomeController.GenerateData();
            var product = HomeController.products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
