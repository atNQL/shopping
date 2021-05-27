using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElectricalShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElectricalShop.Controllers
{
    public class PaymentController : BaseController
    {
        public IActionResult Index()
        {
            return View(app.PaymentRepo.GetPayments());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Payment obj)
        {
            app.PaymentRepo.Add(obj);
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            app.PaymentRepo.DeletePayment(id);
            return RedirectToAction("Index");
        }
    }
}