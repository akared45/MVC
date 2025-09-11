using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using StudentMVC.Services;
using System.ComponentModel.DataAnnotations;
using RegexValidators;

namespace StudentMVC.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController()
        {
            StudentService.GenerateRandomStudents();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (!Validation.IsValidId(student.Id))
            {
                ModelState.AddModelError("Id", "Invalid ID.");
            }
            if (!Validation.IsValidName(student.FirstName))
            {
                ModelState.AddModelError("FirstName", "Invalid First Name.");
            }
            if (!Validation.IsValidName(student.MiddleName))
            {
                ModelState.AddModelError("MiddleName", "Invalid Middle Name.");
            }
            if (!Validation.IsValidName(student.LastName))
            {
                ModelState.AddModelError("LastName", "Invalid Last Name.");
            }
            if (!Validation.IsValidBirthday(student.Birthday))
            {
                ModelState.AddModelError("Birthday", "Invalid Birthday.");
            }
            if (!Validation.IsValidEClass(student.EClass))
            {
                ModelState.AddModelError("EClass", "Invalid Class.");
            }
            if (!Validation.IsValidPhone(student.Phone))
            {
                ModelState.AddModelError("Phone", "Invalid Phone.");
            }
            if (!Validation.IsValidEmail(student.Email))
            {
                ModelState.AddModelError("Email", "Invalid Email.");
            }

            if (ModelState.IsValid)
            {
                StudentService.Add(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 20;
            var students = StudentService.GetPaged(page, pageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = StudentService.TotalPages(pageSize);
            return View(students);
        }

    }
}
