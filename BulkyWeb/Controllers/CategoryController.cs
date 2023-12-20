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
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                // Custom Validation
                // suppose anyone can put same numbers in name textbox and display order textbox
                // lets say we have put name = 55 and display order = 55
                // so thats why below validation is given
                ModelState.AddModelError("name", "The Display Order can not exactly match the name");
            }

            if (obj.Name.ToLower() == "test")
            {
                //  ModelState.AddModelError("", "Test is invalid value"); => here 1st parameter i.e. Key it is empty.

                ModelState.AddModelError("", "Test is invalid value");
            }

            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);

                //Actual saving the data to database which is filled by UI
                _db.SaveChanges();

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Category");

                return RedirectToAction("Index");
            }

            return View();  
        }
    }
}
