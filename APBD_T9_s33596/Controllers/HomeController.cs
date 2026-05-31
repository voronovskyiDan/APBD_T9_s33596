using System.Diagnostics;
using APBD_T9_s33596.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace APBD_T9_s33596.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
