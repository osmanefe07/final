using Microsoft.AspNetCore.Mvc;
using final.Models;
using final.Models;
using final.ViewModels;
    
namespace final.Controllers
{
    public class MarkaController : Controller
    {
        private readonly AppDbContext _context;

        public MarkaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var productModels = _context.Marka.Select(x => new MarkaModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
            }).ToList();

            return View(productModels);
        }

        public IActionResult PageList(string searchText = "", int page = 1, int size = 6)
        {
            var products = _context.Marka.Where(s => s.Name.ToLower().Contains(searchText.ToLower())).AsQueryable();

            int pageskip = (page - 1) * size;
            var productModels = products.Skip(pageskip).Take(size).Select(x => new MarkaModel()
            {
                Id = x.Id,
                Name = x.Name,
                Title = x.Title,
                Description = x.Description,
                Price = x.Price,
          
            }).ToList();
            int recordCount = products.Count();
            int pageCount = (int)Math.Ceiling(Convert.ToDecimal(recordCount / size));


            ViewBag.PageCount = pageCount;
            ViewBag.RecordCount = recordCount;
            ViewBag.Page = page;
            ViewBag.Size = size;
            ViewBag.SearchText = searchText;

            return View(productModels);
        }
    }
}
