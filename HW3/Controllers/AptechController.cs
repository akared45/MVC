using HW3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW3.Controllers
{
    public class AptechController:Controller
    {
        public IActionResult Index()
        {
            ViewData["Address"] = "123 Đường ABC, Quận 1, TP.HCM";
            ViewData["Phone"] = "(+84) 123 456 789";
            ViewData["Email"] = "info@aptech.vn";

            return View();
        }
        public IActionResult Student()
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Nguyễn Văn A", Email = "a@email.com", Phone = "0123456789" },
                new Student { Id = 2, Name = "Trần Thị B", Email = "b@email.com", Phone = "0987654321" },
                new Student { Id = 3, Name = "Lê Văn C", Email = "c@email.com", Phone = "0369852147" }
            };

            return View("StudentList", students);
        }
        public IActionResult StudentDetail(int id)
        {
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Nguyễn Văn A", Email = "a@email.com", Phone = "0123456789" },
                new Student { Id = 2, Name = "Trần Thị B", Email = "b@email.com", Phone = "0987654321" },
                new Student { Id = 3, Name = "Lê Văn C", Email = "c@email.com", Phone = "0369852147" }
            };

            var student = students.Find(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
    }
}

