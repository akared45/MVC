using Microsoft.EntityFrameworkCore;
using Student_DB.Models;

namespace Student_DB.Data
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Score> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<Score>().ToTable("Scores");
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            var subjects = new[]
            {
                new Subject { SubjectId = 1, SubjectName = "Mathematics", SubjectCode = "MATH" },
                new Subject { SubjectId = 2, SubjectName = "Science", SubjectCode = "SCI" },
                new Subject { SubjectId = 3, SubjectName = "English", SubjectCode = "ENG" },
                new Subject { SubjectId = 4, SubjectName = "History", SubjectCode = "HIS" },
                new Subject { SubjectId = 5, SubjectName = "Art", SubjectCode = "ART" }
            };
            modelBuilder.Entity<Subject>().HasData(subjects);
            var students = new List<Student>();
            var random = new Random();
            var names = new[] { "Anh", "Bình", "Chi", "Dũng", "Hạnh", "Khánh", "Lan", "Minh", "Ngọc", "Phúc" };
            var surnames = new[] { "Nguyễn", "Trần", "Lê", "Phạm", "Hoàng", "Huỳnh", "Phan", "Vũ", "Đặng", "Bùi" };
            var grades = new[] { "Grade 9", "Grade 10", "Grade 11", "Grade 12" };
            var classes = new[] { "Class A", "Class B", "Class C", "Class D" };

            for (int i = 1; i <= 150; i++)
            {
                students.Add(new Student
                {
                    StudentId = i,
                    Name = $"{names[random.Next(names.Length)]} {surnames[random.Next(surnames.Length)]}",
                    GradeClass = $"{grades[random.Next(grades.Length)]} - {classes[random.Next(classes.Length)]}"
                });
            }
            modelBuilder.Entity<Student>().HasData(students);
            var scores = new List<Score>();
            var scoreId = 1;

            foreach (var student in students)
            {
                for (int subjectId = 1; subjectId <= 5; subjectId++)
                {
                    scores.Add(new Score
                    {
                        ScoreId = scoreId++,
                        StudentId = student.StudentId,
                        SubjectId = subjectId,
                        Mark = random.Next(50, 100)
                    });
                }
            }
            modelBuilder.Entity<Score>().HasData(scores);
        }
    }
}
