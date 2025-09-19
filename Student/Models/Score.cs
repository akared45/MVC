namespace Student.Models
{
    public class Score
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public float ScoreSubject { get; set; }
    }
}
