using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using final.Models;
using final.ViewModels;

namespace final.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        private readonly AppDbContext _context;

        public CarController(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        [Authorize]
        public IActionResult Index()
        {
            var carsModel = _context.Cars.Select(x => new CarModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description=x.Description,
                Price=x.Price,
                Status = x.Status

            }).ToList();
            return View(carsModel);
        }
        public IActionResult Detail(int id)
        {
            var carModel = _context.Cars.Select(x => new CarModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description =x.Description, 
                Price = x.Price,
                Status = x.Status

            }).SingleOrDefault(x => x.Id == id);
            return View(carModel);

        }
        public IActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insert(CarModel model)
        {
            var car = new Car();
            car.Name = model.Name;
            car.Title = model.Title;
            car.Description = model.Description;
            car.Price = model.Price;
            car.Status = model.Status;

            _context.Cars.Add(car);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var carModel = _context.Cars.Select(x => new CarModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description = x.Description,
                
                Price = x.Price,
                Status = x.Status

            }).SingleOrDefault(x => x.Id == id);
            return View(carModel);
        }

        [HttpPost]
        public IActionResult Edit(CarModel model)
        {
            var car = _context.Cars.SingleOrDefault(x => x.Id == model.Id);
            car.Name = model.Name;
            car.Title = model.Title;
            car.Description = model.Description;   
            car.Price = model.Price;
            car.Status = model.Status;

            _context.Cars.Update(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var carModel = _context.Cars.Select(x => new CarModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
                Status = x.Status

            }).SingleOrDefault(x => x.Id == id);
            return View(carModel);
        }

        [HttpPost]
        public IActionResult Delete(CarModel model)
        {
            var car = _context.Cars.SingleOrDefault(x => x.Id == model.Id);
            _context.Cars.Remove(car);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult ChangeStatus(int id, bool st)
        {
            var car = _context.Cars.SingleOrDefault(x => x.Id == id);
            car.Status = st;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}