using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCoreAppFramework.Areas.SiteAdmin.Controllers
{
    public class HomeController : Controller
    {
        [Area("SiteAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}