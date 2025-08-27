using HW3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HW3.Controllers
{
    public class VietnamController:Controller
    {
        [Route("Vietnam/thanhpho")]
        public IActionResult CityList()
        {
            var cities = new List<City>
            {
                new City { Id = 1, Name = "Hà Nội", Region = "Miền Bắc", Population = 8000000 },
                new City { Id = 2, Name = "TP. Hồ Chí Minh", Region = "Miền Nam", Population = 9000000 },
                new City { Id = 3, Name = "Đà Nẵng", Region = "Miền Trung", Population = 1500000 },
                new City { Id = 4, Name = "Hải Phòng", Region = "Miền Bắc", Population = 2000000 },
                new City { Id = 5, Name = "Cần Thơ", Region = "Miền Nam", Population = 1200000 }
            };

            return View(cities);
        }
    }
}
