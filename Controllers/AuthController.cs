using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

using ElectricalShop.Models;

namespace ElectricalShop.Controllers
{
    public class AuthController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("signin");
        }
        
        public IActionResult Index()
        {
            return View(app.MemberRepo.GetMemberById(User.FindFirst(ClaimTypes.NameIdentifier).Value));
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Member obj)
        {
            app.MemberRepo.Add(obj);
            return RedirectToAction("signin");
        }

        public IActionResult SignIn()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult SignIn(SignInModel obj)
        {
            int ret;
            Member member = app.MemberRepo.SignIn(obj.Usr, obj.Pwd, out ret);
            if (member != null)
            {

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, member.Id));
                claims.Add(new Claim(ClaimTypes.Name, member.Username));
                claims.Add(new Claim(ClaimTypes.Email, member.Email));
                List<Role> roles = app.RoleRepo.GetRolesByMemberId(member.Id);
                foreach (Role role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Name));
                }
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    //IsPersistent = true
                    IsPersistent = obj.Remember
                };
                HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity), properties);

                if (!string.IsNullOrEmpty(Request.Cookies["cart"]))
                {
                    app.CartRepo.UpdateMemberId2Cart(Request.Cookies["cart"], member.Id);
                }
                return Redirect("/");
            }
            else
            {
                string[] errros =
                {
                    "Username not found",
                    "Sign In Failed Password"
                };
                ModelState.AddModelError("error", errros[ret]);
                return View(obj);
            }
        }

        public IActionResult Edit(string id)
        {
            return View(app.MemberRepo.GetMemberById(id));
        }

        [HttpPost]
        public IActionResult Edit(Member obj)
        {
            app.MemberRepo.UpdateMemberInfor(obj);
            return RedirectToAction("index");
        }
    }
}