using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // Retrieving data from database and showing it into the category view page.
            // Also Refer the code of Index.cshtml file from Category folder how we can show this list on view page.

            List<Category> objCategoryList = _db.Categories.ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            _db.Categories.Add(obj);

            //Actual saving the data to database which is filled by UI

            _db.SaveChanges();

            // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
            //return RedirectToAction("Index","Category");

            return RedirectToAction("Index");
        }
    }
}
