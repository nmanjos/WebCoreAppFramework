using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebCoreAppFramework.Authorization;
using WebCoreAppFramework.Data;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Options;
using WebCoreAppFramework.ViewModels;

namespace WebCoreAppFramework.Controllers
{
    [Authorize(Permissions.AdminUser.Read)]
    public class UserAdminController : Controller
    {
        private readonly ILogger Logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;
        

        public UserAdminController(ApplicationDbContext context, ILogger<UserAdminController> logger)
        {
            dbContext = context;
            Logger = logger;
            
        }


        
        public IActionResult Index()
        {
            IQueryable<UserIndexViewModel> Model = dbContext.Users.Select(x => new UserIndexViewModel { UserId = x.Id, UserName = x.UserName, UserRoles = x.UserRoles.Select(q => new UserRolesView { Name = q.Role.Name }).ToList() });

            return View(Model);
        }
    }
}