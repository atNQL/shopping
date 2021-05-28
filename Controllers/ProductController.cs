using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectricalShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectricalShop.Controllers
{
    public class ProductController : BaseController
    {
        // TODO...
        public IActionResult Index(int id = 1)
        {
            //return View(app.ProductRepo.GetProducts());
            int total;
            int productPerPage = 6;
            List<Product> list = app.ProductRepo.GetProductsPagination(id, productPerPage, out total);
            ViewBag.total = total;
            ViewBag.productPerPage = productPerPage;
            return View(list);
        }

        [HttpPost]
        public IActionResult Create(Product obj, IFormFile f)
        {
            string fileName = Upload(f);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }
            app.ProductRepo.AddProduct(obj);
            return RedirectToAction("index");
        }

        public IActionResult Create()
        {
            ViewBag.categoryId = new SelectList(app.CategoryRepo.GetCategories(), "Id", "Name");
            return View();
        }


        public IActionResult Search(string q)
        {
            return View(app.ProductRepo.SearchProducts(q));
        }

        public IActionResult ProductsByCategory(int id)
        {
            ViewBag.CategoryName = app.CategoryRepo.GetCategoryById(id).Name;
            return View(app.ProductRepo.GetProductsByCategory(id));
        }

        public IActionResult Detail(int id)
        {
            return View(app.ProductRepo.GetProductById(id));
        }

        [HttpPost]
        public IActionResult Detail(Product obj)
        {
            var cookie = Request.Cookies["cart"];
            if (cookie is null)
            {
                cookie = Helper.RandomString(32);
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Append("cart", cookie, option);
            }

            Cart cart = new Cart
            {
                Id = cookie,
                ProductId = obj.Id,
                Quantity = obj.Quantity
            };

            if (User.Identity.IsAuthenticated)
            {
                cart.MemberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            app.CartRepo.Add(cart);

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.categoryId = new SelectList(app.CategoryRepo.GetCategories(), "Id", "Name");
            return View(app.ProductRepo.GetProductById(id));
        }

        [HttpPost]
        public IActionResult Edit(Product obj, IFormFile f)
        {
            string fileName = Upload(f);
            if (fileName != null)
            {
                obj.ImageUrl = fileName;
            }
            app.ProductRepo.Edit(obj);
            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            app.ProductRepo.Delete(id);
            return RedirectToAction("index");
        }
    }
}