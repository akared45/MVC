using System.ComponentModel.DataAnnotations;

namespace phao.Models{
    public class Enrollment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ngày đăng ký là bắt buộc")]
        public DateTime EnrollmentDate { get; set; }

        [Required(ErrorMessage = "Tên sinh viên là bắt buộc")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên phải từ 3-100 ký tự")]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khóa học")]
        public int CourseId { get; set; }

        [Range(0.0, 4.0, ErrorMessage = "Điểm phải từ 0.0 đến 4.0")]
        public decimal? Grade { get; set; }

        public Course? Course { get; set; }
    }

}
