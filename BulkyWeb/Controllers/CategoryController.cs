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
    }
}
