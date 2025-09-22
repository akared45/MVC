using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_DB.Data;
using Student_DB.Models;

namespace Student_DB.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _context;
        private readonly ILogger<StudentController> _logger;

        public StudentController(StudentDbContext context, ILogger<StudentController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sortOrder = "name", int page = 1, int pageSize = 5, string searchString = "")
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;
            ViewData["NameSortParm"] = sortOrder == "name" ? "name_desc" : "name";
            ViewData["MathSortParm"] = sortOrder == "math" ? "math_desc" : "math";
            ViewData["ScienceSortParm"] = sortOrder == "science" ? "science_desc" : "science";
            ViewData["EnglishSortParm"] = sortOrder == "english" ? "english_desc" : "english";
            ViewData["HistorySortParm"] = sortOrder == "history" ? "history_desc" : "history";
            ViewData["ArtSortParm"] = sortOrder == "art" ? "art_desc" : "art";
            ViewData["AverageSortParm"] = sortOrder == "average" ? "average_desc" : "average";

            var students = await _context.Students.ToListAsync();
            var scores = await _context.Scores.ToListAsync();
            var subjects = await _context.Subjects.ToListAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students
                    .Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                             || s.GradeClass.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var studentData = new List<dynamic>();

            foreach (var student in students)
            {
                var studentScores = scores.Where(s => s.StudentId == student.StudentId).ToList();

                var math = studentScores.FirstOrDefault(s => s.SubjectId == 1)?.Mark ?? 0;
                var science = studentScores.FirstOrDefault(s => s.SubjectId == 2)?.Mark ?? 0;
                var english = studentScores.FirstOrDefault(s => s.SubjectId == 3)?.Mark ?? 0;
                var history = studentScores.FirstOrDefault(s => s.SubjectId == 4)?.Mark ?? 0;
                var art = studentScores.FirstOrDefault(s => s.SubjectId == 5)?.Mark ?? 0;
                var average = (math + science + english + history + art) / 5.0;

                studentData.Add(new
                {
                    StudentId = student.StudentId,
                    Name = student.Name,
                    GradeClass = student.GradeClass,
                    Math = math,
                    Science = science,
                    English = english,
                    History = history,
                    Art = art,
                    Average = average
                });
            }

            var sortedData = sortOrder.ToLower() switch
            {
                "name_desc" => studentData.OrderByDescending(s => s.Name).ToList(),
                "math" => studentData.OrderBy(s => s.Math).ToList(),
                "math_desc" => studentData.OrderByDescending(s => s.Math).ToList(),
                "science" => studentData.OrderBy(s => s.Science).ToList(),
                "science_desc" => studentData.OrderByDescending(s => s.Science).ToList(),
                "english" => studentData.OrderBy(s => s.English).ToList(),
                "english_desc" => studentData.OrderByDescending(s => s.English).ToList(),
                "history" => studentData.OrderBy(s => s.History).ToList(),
                "history_desc" => studentData.OrderByDescending(s => s.History).ToList(),
                "art" => studentData.OrderBy(s => s.Art).ToList(),
                "art_desc" => studentData.OrderByDescending(s => s.Art).ToList(),
                "average" => studentData.OrderBy(s => s.Average).ToList(),
                "average_desc" => studentData.OrderByDescending(s => s.Average).ToList(),
                _ => studentData.OrderBy(s => s.Name).ToList()
            };

            var totalCount = sortedData.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            var pagedData = sortedData.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var allAverages = studentData.Select(s => (double)s.Average).ToList();
            var averageScore = allAverages.Any() ? allAverages.Average() : 0;
            var topStudent = allAverages.Any() ? allAverages.Max() : 0;
            var lowScores = allAverages.Count(a => a < 60);

            ViewData["TotalStudents"] = totalCount;
            ViewData["AverageScore"] = averageScore.ToString("F1");
            ViewData["TopStudent"] = topStudent.ToString("F1");
            ViewData["LowScores"] = lowScores;
            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = totalPages;
            ViewData["PageSize"] = pageSize;

            return View(pagedData);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var scores = _context.Scores.Where(s => s.StudentId == id);
                _context.Scores.RemoveRange(scores);

                var student = await _context.Students.FindAsync(id);
                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting student: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var scores = await _context.Scores.Where(s => s.StudentId == id).ToListAsync();
            ViewBag.Math = scores.FirstOrDefault(s => s.SubjectId == 1)?.Mark ?? 0;
            ViewBag.Science = scores.FirstOrDefault(s => s.SubjectId == 2)?.Mark ?? 0;
            ViewBag.English = scores.FirstOrDefault(s => s.SubjectId == 3)?.Mark ?? 0;
            ViewBag.History = scores.FirstOrDefault(s => s.SubjectId == 4)?.Mark ?? 0;
            ViewBag.Art = scores.FirstOrDefault(s => s.SubjectId == 5)?.Mark ?? 0;

            return View(student);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int StudentId, string Name, string GradeClass,
            double Math, double Science, double English, double History, double Art)
        {
            var student = await _context.Students.FindAsync(StudentId);
            if (student == null)
            {
                return NotFound();
            }

            student.Name = Name;
            student.GradeClass = GradeClass;

            await UpdateScore(StudentId, 1, Math);
            await UpdateScore(StudentId, 2, Science);
            await UpdateScore(StudentId, 3, English);
            await UpdateScore(StudentId, 4, History);
            await UpdateScore(StudentId, 5, Art);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateScore(int studentId, int subjectId, double mark)
        {
            var score = await _context.Scores
                .FirstOrDefaultAsync(s => s.StudentId == studentId && s.SubjectId == subjectId);

            if (score != null)
            {
                score.Mark = mark;
            }
            else
            {
                _context.Scores.Add(new Score
                {
                    StudentId = studentId,
                    SubjectId = subjectId,
                    Mark = mark
                });
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Name, string GradeClass,
            double Math, double Science, double English, double History, double Art)
        {
            var student = new Student
            {
                Name = Name,
                GradeClass = GradeClass
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            await AddScore(student.StudentId, 1, Math);
            await AddScore(student.StudentId, 2, Science);
            await AddScore(student.StudentId, 3, English);
            await AddScore(student.StudentId, 4, History);
            await AddScore(student.StudentId, 5, Art);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task AddScore(int studentId, int subjectId, double mark)
        {
            _context.Scores.Add(new Score
            {
                StudentId = studentId,
                SubjectId = subjectId,
                Mark = mark
            });
        }
    }
}