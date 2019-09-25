using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCEmailService.ViewModels;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            List<HomeAboutViewModel> Model = new List<HomeAboutViewModel>();

            Model.Add(new HomeAboutViewModel
            {
                Title = "About Title 1",
                Description = "About Description 1",
                ImagePath = "https://cdn.slidemodel.com/wp-content/uploads/0055-flat-design-banners-powerpoint-templates-16x9-1.jpg"
            });
            Model.Add(new HomeAboutViewModel
            {
                Title = "About Title 2",
                Description = "About Description 2",
                ImagePath = "https://cdn.slidemodel.com/wp-content/uploads/0055-flat-design-banners-powerpoint-templates-16x9-2.jpg"
            });
            Model.Add(new HomeAboutViewModel
            {
                Title = "About Title 3",
                Description = "About Description 3",
                ImagePath = "https://cdn.slidemodel.com/wp-content/uploads/0055-flat-design-banners-powerpoint-templates-16x9-3.jpg"
            });

            IEnumerable<HomeAboutViewModel> xxx = Model.Select(w => new HomeAboutViewModel { Title = w.Title, Description = w.Description, ImagePath = w.ImagePath });
            return View(xxx);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
