using HW4.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace HW4.Controllers
{
    public class HomeController : Controller
    {
        private List<Movie> ListMovies()
        {
            return new List<Movie>
            {
                new Movie
                 {
                    Id = 1,
                    Title = "Avengers: Endgame",
                    ShortDescription = "The Avengers superheroes team up to reverse the damage Thanos has caused.",
                    Price = 15.99m,
                    Director = "Anthony Russo, Joe Russo",
                    TrailerUrl = "https://www.youtube.com/embed/TcMBFSGVi1c",
                    WatchUrl = "#",
                    ImageUrl = "/images/avenger.jpg",
                    FullDescription = "After the devastating events of Avengers: Infinity War, the universe is in ruins. With the help of their remaining allies, the Avengers assemble once again to reverse Thanos' actions and restore balance to the universe."
                },
                new Movie
                {
                    Id = 2,
                    Title = "The Dark Knight",
                    ShortDescription = "Batman faces the Joker, a mad criminal bent on causing chaos in Gotham.",
                    Price = 12.99m,
                    Director = "Christopher Nolan",
                    TrailerUrl = "https://www.youtube.com/embed/EXeTwQWrcwY",
                    WatchUrl = "#",
                    ImageUrl = "",
                    FullDescription = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of his greatest psychological and physical challenges to fight injustice."
                },
                new Movie
                {
                    Id = 3,
                    Title = "Inception",
                    ShortDescription = "Một tên trộm có khả năng đột nhập vào giấc mơ của người khác được giao nhiệm vụ bất khả thi.",
                    Price = 14.99m,
                    Director = "Christopher Nolan",
                    TrailerUrl = "https://www.youtube.com/embed/YoHD9XEInc0",
                    WatchUrl = "#",
                    ImageUrl = "",
                    FullDescription = "Dom Cobb is a skilled thief, the best at secret extraction: stealing valuable ideas from the subconscious while dreams are taking place. Cobb's unique abilities have made him a dangerous figure in this emerging business world."
                },
            };
        }
        public IActionResult Index()
        {
            var movies = ListMovies();
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movies = ListMovies();
            var movie = movies.Find(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
