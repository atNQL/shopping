using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ElectricalShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectricalShop.Controllers
{
    public class InvoiceController : BaseController
    {
        
        public IActionResult Index()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Cart = app.CartRepo.GetCartsByMember(memberId);

            ViewBag.Payments = app.PaymentRepo.GetPayments();

            return View();
        }

        [HttpPost]
        public IActionResult Index(Invoice obj)
        {
            string id = Helper.RandomString(32);
            obj.Id = id;
            obj.MemberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            app.InvoiceRepo.AddInvoice(obj);

            List<Cart> carts = app.CartRepo.GetCartsByMember(obj.MemberId);
            foreach (var item in carts)
            {
                InvoiceDetail invoiceDetail = new InvoiceDetail
                {
                    InvoiceId = obj.Id,
                    ProductId = item.ProductId,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                app.InvoiceDetailRepo.AddInvoiceDetail(invoiceDetail);
                app.CartRepo.DeleteProductInCart(item.Id,item.ProductId);
            }
            return RedirectToAction("index","cart");
        }
        
        public IActionResult Invoice()
        {
            string memberId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Invoice> invoices = app.InvoiceRepo.GetInvoceByMemberId(memberId);
            foreach (var item in invoices)
            {
                item.InvoiceDetail = app.InvoiceDetailRepo.GetInvoceDetailByInvoiceId(item.Id);
            }
            ViewBag.Infor = app.MemberRepo.GetMemberById(memberId);
            return View(invoices);
        }

    }
}