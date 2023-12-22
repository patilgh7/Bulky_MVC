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


        // Create Operation

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


                // Create TempData for display notification where the data saved successfully or not.
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }

            return View();  
        }

        //  ---------------------------------xxxxx-------------------------------------------

        // For Edit 

        /*
         * int? id
         * id parameter is nullable
         * when you add question mark after any datatype like int,float or class like Category
         * int? represents a nullable integer or flaot? represents a nullable float or Category? then it is said to be nullable
         * nullableInt can hold an integer value or be explicitly set to null.
         * In summary, a nullable type is a type that can represent both a valid value of its underlying type and the absence of a value (null). 
         * This feature is particularly helpful when dealing with scenarios where the presence or absence of data needs to be explicitly managed in the code. 
         * The code checks if the id is null. If it is, it returns a bad request result, 
         * indicating that the provided identifier is invalid. 
         * If the id is not null, the method proceeds with further processing.
         */

        // Update Operation
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            // 3 Approaches for finding the id for editing....but best approach is using FirstOrDefault method

            // Using Find method you can only deal with primary key
            Category? categoryFromDb = _db.Categories.Find(id);

            // Using FirstOrDefault method you can deal with any parameter like Id, Name anything
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            // Using Where method
            // Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
       
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);

                //Actual saving the updated data to database which is filled by UI
                _db.SaveChanges();

                // Create TempData for display notification where the data updated successfully or not.
                TempData["success"] = "Category updated successfully";

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Category");

                return RedirectToAction("Index");
            }

            return View();
        }


        //  ---------------------------------xxxxx-------------------------------------------

        // Delete Operation
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

         
            // Using Find method you can only deal with primary key
            Category? categoryFromDb = _db.Categories.Find(id);

            // Using FirstOrDefault method you can deal with any parameter like Id, Name anything
            // Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            // Using Where method
            // Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id); 

            if (obj == null) 
            {
                return NotFound();
            }

            _db.Categories.Remove(obj);

            //Actual deleting the data to database which is deleted by UI
            _db.SaveChanges();

            // Create TempData for display notification where the data deleted successfully or not.
            TempData["success"] = "Category deleted successfully";

            // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
            //return RedirectToAction("Index","Category");

            return RedirectToAction("Index");

        }


    }
}
