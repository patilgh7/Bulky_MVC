using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
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

       
        // Upsert Operation => Update + Insert => It is combination of Create and Edit Functionality
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if(id == null || id == 0)
            {
                // Create
                return View(productVM);
            }
            else
            {
                // Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

           
        }


        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
         
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVM.Product);

                //Actual saving the data to database which is filled by UI
                _unitOfWork.Save();

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Product");


                // Create TempData for display notification where the data saved successfully or not.
                TempData["success"] = "Product created successfully";

                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                

                return View(productVM);
            }

           
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

