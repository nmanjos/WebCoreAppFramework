using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCEmailService.ViewModels;
using WebCoreAppFramework.Data;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Services;
using WebCoreAppFramework.ViewModels;

namespace WebCoreAppFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger Logger;
        private readonly AppUserManager userManager;
        private readonly ApplicationDbContext dbContext;

        public HomeController(ApplicationDbContext context, ILogger<UserController> logger, AppUserManager UserManager)
        {
            dbContext = context;
            Logger = logger;
            userManager = UserManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> SetLocation(GeoLocationViewModel geoLocation)
        {
            bool result = false;
            if (User.Identity.IsAuthenticated)
            {
                var UserName = User.Identity.Name;
                result = await userManager.SetCurrentUserLocation(UserName, geoLocation.latitude, geoLocation.longitude);
            }
            
            
            return Json(geoLocation);
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
