using System.Linq;
using System.Threading.Tasks;
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
    [Area("SiteAdmin")]
    public class UserController : Controller
    {
        private readonly ILogger Logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext dbContext;


        public UserController(ApplicationDbContext context, ILogger<UserController> logger, UserManager<ApplicationUser> UserManager)
        {
            dbContext = context;
            Logger = logger;
            userManager = UserManager;


        }

        public async Task<IActionResult> Index()
        {
            IQueryable<UserIndexViewModel> Model = dbContext.Users.Select(x => new UserIndexViewModel { UserId = x.Id, UserName = x.UserName, UserRoles = x.UserRoles.Select(q => new UserRolesViewModel { Name = q.Role.Name }).ToList() });

            return View(Model);
        }


        public async Task<IActionResult> Details(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                UserDetailsViewModel UserDetails = new UserDetailsViewModel
                {
                    UserName = user.UserName,
                    AccessFailedCount = user.AccessFailedCount,
                    Claims = user.Claims,
                    EmailConfirmed = user.EmailConfirmed,
                    Id = Id,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEnd = user.LockoutEnd,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserRoles = user.UserRoles
                };
                return View(UserDetails);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user != null)
            {
                UserDetailsViewModel UserDetails = new UserDetailsViewModel
                {
                    UserName = user.UserName,
                    AccessFailedCount = user.AccessFailedCount,
                    Claims = user.Claims,
                    EmailConfirmed = user.EmailConfirmed,
                    Id = Id,
                    LockoutEnabled = user.LockoutEnabled,
                    LockoutEnd = user.LockoutEnd,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                    UserRoles = user.UserRoles
                };
                return View(UserDetails);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] UserDetailsViewModel UserDetails)
        {
            var usr = UserDetails.UserName;
            return Ok();
        }
    }
}