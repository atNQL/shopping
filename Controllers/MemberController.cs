using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricalShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectricalShop.Controllers
{
    public class MemberController : BaseController
    {
        public IActionResult Index()
        {
            List<Member> members = app.MemberRepo.GetMembers();
            return View(app.MemberRepo.GetMembers());
        }

        public IActionResult MemberInRole(string id)
        {
            Member obj = app.MemberRepo.GetMemberById(id);
            obj.Roles = app.MemberRepo.GetMemberInRoles(id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult MemberInRole(MemberInRole obj)
        {
            //return Json(obj);
            return Json(app.MemberInRoleRepo.Add(obj));
        }

        public IActionResult AddMembers()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMembers(DateTime dated, string[] username, string[] password, string[] email, bool[] gender)
        {
            List<Member> list = new List<Member>();

            for (int i = 0; i < username.Length; i++)
            {
                if (username[i] != null)
                {
                    list.Add(new Member
                    {
                        Username = username[i],
                        Password = password[i],
                        Email = email[i],
                        Gender = gender[i],
                    });
                }

            }
            app.MemberRepo.AddMembers(list);

            return RedirectToAction("index");
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddRole(Role obj)
        {
            app.RoleRepo.Add(obj);
            return RedirectToAction("index");
        }


        public IActionResult Delete(string id)
        {
            app.MemberRepo.DeleteMember(id);
            return RedirectToAction("index");

        }

    }
}