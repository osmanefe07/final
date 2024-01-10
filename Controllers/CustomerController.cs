using Microsoft.AspNetCore.Mvc;
using final.Models;
using final.ViewModels;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;

namespace final.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context) 
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerListAjax() 
        {
            var customerModels = _context.Customers.Select(x => new CustomerModel()
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Email = x.Email,
                Phone = x.Phone,
            }).ToList();
            return Json(customerModels);

        }
        public IActionResult CustomerByIdAjax(int id)
        {
            var customerModel = _context.Customers.Where(s => s.Id == id).Select(x => new CustomerModel()
            {
                Id = x.Id,
                Name= x.Name,
                Address= x.Address,
                Email = x.Email,
                Phone = x.Phone,
            }).SingleOrDefault();

            return Json(customerModel);
        }
        [HttpPost]
        public IActionResult CustomerAddEditAjax(CustomerModel model)
        {
            var sonuc = new SonucModel();
            if (model.Id == 0)
            {

                if (_context.Customers.Count(c => c.Name == model.Name) > 0)
                {
                    sonuc.Status = false;
                    sonuc.Message = "Girilen Başlık Kayıtlıdır!";
                    return Json(sonuc);
                }

                var customer = new Customer();
                customer.Id = model.Id;
                customer.Name = model.Name;
                customer.Address = model.Address;
                customer.Email = model.Email;
                customer.Phone = model.Phone;
                _context.Customers.Add(customer);
                _context.SaveChanges();
                sonuc.Status = true;
                sonuc.Message = "Müşteri Eklendi";
            }
            else
            {
                var customer = _context.Customers.FirstOrDefault(x => x.Id == model.Id);
                customer.Id = model.Id;
                customer.Name = model.Name;
                customer.Address = model.Address;
                customer.Email = model.Email;
                customer.Phone = model.Phone;
                _context.SaveChanges();
                sonuc.Status = true;
                sonuc.Message = "Müşteri Güncellendi";
            }

            return Json(sonuc);
        }
        public IActionResult CustomerRemoveAjax(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            var sonuc = new SonucModel();
            sonuc.Status = true;
            sonuc.Message = "Müşteri Silindi";
            return Json(sonuc);
        }
    }
}
