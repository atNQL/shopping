using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricalShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectricalShop.Controllers
{

    public class CategoryController : BaseController
    {
        public IActionResult Index()
        {
            return View(app.CategoryRepo.GetCategories());
        }

        //[Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj, IFormFile f)
        {
            string fileName = Upload(f);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }
            app.CategoryRepo.Add(obj);
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            return View(app.CategoryRepo.GetCategoryById(id));
        }

        [HttpPost]
        public IActionResult Edit(Category obj, IFormFile f)
        {
            string fileName = Upload(f);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }
            app.CategoryRepo.Edit(obj);
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            app.CategoryRepo.Delete(id);

            return RedirectToAction("index");
        }
    }
}
