using Microsoft.AspNetCore.Mvc;

namespace Student.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
