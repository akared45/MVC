namespace HW4.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }
        public string Director { get; set; }
        public string TrailerUrl { get; set; }
        public string WatchUrl { get; set; }
        public string ImageUrl { get; set; }
        public string FullDescription { get; set; }
    }
}
