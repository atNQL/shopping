using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace ElectricalShop.Controllers
{
    public class CartController : BaseController
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                return View(app.CartRepo.GetCartsByMember(memberId));
            }
            else
            {
                string cookieValue = Request.Cookies["cart"];
                if (cookieValue is null)
                {
                    return Redirect("/");
                }
                else
                {
                    //GetCarts by Cookie
                    return View(app.CartRepo.GetCarts(cookieValue));
                }
            }
        }

        public IActionResult Delete(int id)
        {
            app.CartRepo.DeleteProductInCart(Request.Cookies["cart"], id);
            return RedirectToAction("Index");
        }

    }
}