using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            // Retrieving data from database and showing it into the product view page.
            // Also Refer the code of Index.cshtml file from Product folder how we can show this list on view page.

            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objProductList);
        }


        // Create Operation

        public IActionResult Create()
        {
            // Projections in EF Core
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });

            //ViewBag.CategoryList = CategoryList;

            ViewData["CategoryList"] = CategoryList;

            return View();
        }


        [HttpPost]
        public IActionResult Create(Product obj)
        {
         
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);

                //Actual saving the data to database which is filled by UI
                _unitOfWork.Save();

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Product");


                // Create TempData for display notification where the data saved successfully or not.
                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        //  ---------------------------------xxxxx-------------------------------------------

        // For Edit 

        /*
         * int? id
         * id parameter is nullable
         * when you add question mark after any datatype like int,float or class like Product
         * int? represents a nullable integer or flaot? represents a nullable float or Product? then it is said to be nullable
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
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // 3 Approaches for finding the id for editing....but best approach is using FirstOrDefault method

            // Using Find method you can only deal with primary key
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            // Using FirstOrDefault method you can deal with any parameter like Id, Name anything
            // Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            // Using Where method
            // Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);

                //Actual saving the updated data to database which is filled by UI
                _unitOfWork.Save();

                // Create TempData for display notification where the data updated successfully or not.
                TempData["success"] = "Product updated successfully";

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Product");

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
            Product? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            // Using FirstOrDefault method you can deal with any parameter like Id, Name anything
            // Product? productFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            // Using Where method
            // Product? productFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (productFromDb == null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);

            //Actual deleting the data to database which is deleted by UI
            _unitOfWork.Save();

            // Create TempData for display notification where the data deleted successfully or not.
            TempData["success"] = "Product deleted successfully";

            // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
            //return RedirectToAction("Index","Product");

            return RedirectToAction("Index");

        }


    }
}

