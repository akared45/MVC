namespace Student_DB.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public double Mark { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
    }
}