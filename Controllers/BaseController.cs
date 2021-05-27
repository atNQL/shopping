using ElectricalShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ElectricalShop.Controllers
{
    public class BaseController : Controller
    {
        protected AppRepository app = new AppRepository();
        static protected string Upload(IFormFile f)
        {
            if (f != null)
            {
                string path = Directory.GetCurrentDirectory() + "/wwwroot/images/";
                using (Stream stream = System.IO.File.Create(path + f.FileName))
                {
                    f.CopyTo(stream);
                }
                return f.FileName;
            }
            return null;
        }
    }
}