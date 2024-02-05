using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
          
        }
        public IActionResult Index()
        {
            // Retrieving data from database and showing it into the company view page.
            // Also Refer the code of Index.cshtml file from Company folder how we can show this list on view page.

            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

       
        // Upsert Operation => Update + Insert => It is combination of Create and Edit Functionality
        public IActionResult Upsert(int? id)
        {
          
            if(id == null || id == 0)
            {
                // Create
                return View(new Company());
            }
            else
            {
                // Update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }

           
        }


        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
         
            if (ModelState.IsValid)
            {

               

                if(CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }

                

                //Actual saving the data to database which is filled by UI
                _unitOfWork.Save();

                // You can pass action only i.e Index when you are same controller otherwise pass 2nd parameter controller is good practice
                //return RedirectToAction("Index","Company");


                // Create TempData for display notification where the data saved successfully or not.
                TempData["success"] = "Company created successfully";

                return RedirectToAction("Index");
            }
            else
            {
              
                return View(CompanyObj);
            }

           
        }


        //  ---------------------------------xxxxx-------------------------------------------

       

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new {data = objCompanyList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u=> u.Id == id);

            if(companyToBeDeleted == null)
            {
                return Json(new {success  = false, message = "Error while deleting" });
            }


            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();
            
            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion

    }
}

