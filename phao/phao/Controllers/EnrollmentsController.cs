using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using phao.Data;
using phao.Models;

namespace phao.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var enrollments = _context.Enrollments
                .Include(e => e.Course)
                .ToList();

            var totalItems = enrollments.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedEnrollments = enrollments.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedEnrollments);
        }
        public IActionResult Create()
        {
            ViewBag.CourseList = new SelectList(_context.Courses, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                enrollment.EnrollmentDate = DateTime.Now;
                _context.Enrollments.Add(enrollment);
                _context.SaveChanges();
                return RedirectToAction("Index", "Courses");
            }

            ViewBag.CourseList = new SelectList(_context.Courses, "Id", "Title");
            return View(enrollment);
        }
        public IActionResult Edit(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Course)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewBag.CourseList = new SelectList(_context.Courses, "Id", "Title", enrollment.CourseId);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Course");

            if (ModelState.IsValid)
            {
                try
                {
                    var existingEnrollment = _context.Enrollments.Find(id);
                    if (existingEnrollment == null)
                    {
                        return NotFound();
                    }

                    existingEnrollment.StudentName = enrollment.StudentName;
                    existingEnrollment.StudentEmail = enrollment.StudentEmail;
                    existingEnrollment.CourseId = enrollment.CourseId;
                    existingEnrollment.Grade = enrollment.Grade;

                    _context.Update(existingEnrollment);
                    _context.SaveChanges();

                    return RedirectToAction("Index", "Courses");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating enrollment: " + ex.Message);
                }
            }

            ViewBag.CourseList = new SelectList(_context.Courses, "Id", "Title", enrollment.CourseId);
            return View(enrollment);
        }
        public IActionResult Delete(int id)
        {
            var enrollment = _context.Enrollments
                .Include(e => e.Course)
                .FirstOrDefault(e => e.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = _context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}