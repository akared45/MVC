using Bogus;
using StudentMVC.Models;

namespace StudentMVC.Services
{
    public class StudentService
    {
        private static List<Student> _students = new List<Student>();

        public static void GenerateRandomStudents(int count = 100000)
        {
            if (_students.Count > 0) return;

            var studentFaker = new Faker<Student>("vi") 
                .RuleFor(s => s.Id, f => (1 + f.IndexFaker).ToString())
                .RuleFor(s => s.FirstName, f => f.Name.LastName()) 
                .RuleFor(s => s.MiddleName, f => f.PickRandom(new[] { "Văn", "Thị", "Minh", "Quốc", "Hồng", "Anh", "Đức", "Huyền", "Tuấn", "Lan" }))
                .RuleFor(s => s.LastName, f => f.Name.FirstName())
                .RuleFor(s => s.Birthday, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.EClass, f => f.PickRandom(new[] { "KTPM1", "KTPM2", "CNTT1", "CNTT2", "KTMT1", "KTMT2" }))
                .RuleFor(s => s.Phone, f => "0" + f.Random.Number(900000000, 999999999).ToString()) 
                .RuleFor(s => s.Email, (f, s) => $"{s.LastName.ToLower()}{f.Random.Number(1000)}@{f.PickRandom(new[] { "gmail.com", "yahoo.com", "outlook.com", "student.edu.vn" })}");
            _students = studentFaker.Generate(count);
        }

        public static List<Student> GetAll() => _students;

        public static void Add(Student student)
        {
            _students.Add(student);
        }

        public static List<Student> GetPaged(int page, int pageSize = 20)
        {
            return _students.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public static int TotalCount => _students.Count;

        public static int TotalPages(int pageSize = 20) => (int)Math.Ceiling((double)TotalCount / pageSize);
    }
}

