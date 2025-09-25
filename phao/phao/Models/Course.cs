using System.ComponentModel.DataAnnotations;
namespace phao.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tiêu đề là bắt buộc")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Tiêu đề phải từ 5-255 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Số tín chỉ là bắt buộc")]
        [Range(1, 5, ErrorMessage = "Số tín chỉ phải từ 1-5")]
        public int Credits { get; set; }

        [StringLength(100, ErrorMessage = "Tên khoa không quá 100 ký tự")]
        public string? Department { get; set; }

        public List<Enrollment> Enrollments { get; set; } = new();
    }
}
