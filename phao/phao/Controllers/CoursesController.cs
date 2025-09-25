using Microsoft.AspNetCore.Mvc;
using phao.Data;
using phao.Models;

namespace phao.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoursesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string sortOrder, int page = 1, int pageSize = 10)
        {
            SetSortViewBag(sortOrder);

            var courses = _context.Courses.AsQueryable();
            courses = ApplySorting(courses, sortOrder);

            var pagedCourses = ApplyPagination(courses, page, pageSize);

            return View(pagedCourses);
        }

        [HttpPost]
        public IActionResult FilterCourses(int minCredits, int page = 1, int pageSize = 10)
        {
            SetSortViewBag("");

            var courses = _context.Courses.AsQueryable();
            courses = ApplyCreditFilter(courses, minCredits);
            courses = ApplySorting(courses, "");

            var pagedCourses = ApplyPagination(courses, page, pageSize);
            ViewBag.MinCredits = minCredits;

            return View("Index", pagedCourses);
        }

        [HttpPost]
        public IActionResult SearchCourses(string searchTitle, int page = 1, int pageSize = 10)
        {
            SetSortViewBag("");

            var courses = _context.Courses.AsQueryable();
            courses = ApplyTitleSearch(courses, searchTitle);
            courses = ApplySorting(courses, "");

            var pagedCourses = ApplyPagination(courses, page, pageSize);
            ViewBag.SearchTitle = searchTitle;

            return View("Index", pagedCourses);
        }
        private void SetSortViewBag(string sortOrder)
        {
            ViewBag.TitleSortParam = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.CreditsSortParam = sortOrder == "credits" ? "credits_desc" : "credits";
            ViewBag.DepartmentSortParam = sortOrder == "department" ? "department_desc" : "department";
            ViewBag.CurrentSort = sortOrder;
        }

        private IQueryable<Course> ApplySorting(IQueryable<Course> courses, string sortOrder)
        {
            return sortOrder switch
            {
                "title_desc" => courses.OrderByDescending(c => c.Title),
                "credits" => courses.OrderBy(c => c.Credits),
                "credits_desc" => courses.OrderByDescending(c => c.Credits),
                "department" => courses.OrderBy(c => c.Department),
                "department_desc" => courses.OrderByDescending(c => c.Department),
                _ => courses.OrderBy(c => c.Title)
            };
        }

        private IQueryable<Course> ApplyCreditFilter(IQueryable<Course> courses, int minCredits)
        {
            return courses.Where(c => c.Credits >= minCredits);
        }

        private IQueryable<Course> ApplyTitleSearch(IQueryable<Course> courses, string searchTitle)
        {
            if (!string.IsNullOrEmpty(searchTitle))
            {
                return courses.Where(c => c.Title.Contains(searchTitle));
            }
            return courses;
        }

        private List<Course> ApplyPagination(IQueryable<Course> courses, int page, int pageSize)
        {
            var totalItems = courses.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return courses.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
