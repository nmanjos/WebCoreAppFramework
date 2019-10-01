using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebCoreAppFramework.Authorization;
using WebCoreAppFramework.Data;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.ViewModels;

namespace WebCoreAppFramework.Areas.SiteAdmin.Controllers
{


    [Authorize(Permissions.AdminUser.Read)]
    [Area("SiteAdmin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger Logger;
        private readonly RoleManager<ApplicationRole> RoleManager;



        public RolesController(ApplicationDbContext context, ILogger<RolesController> logger, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            Logger = logger;
            RoleManager = roleManager;
        }

        // GET: SiteAdmin/Roles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: SiteAdmin/Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }

            return View(applicationRole);
        }

        // GET: SiteAdmin/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteAdmin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visible,System,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (ModelState.IsValid)
            {

                await RoleManager.CreateAsync(applicationRole);

                return RedirectToAction(nameof(Index));
            }
            return View(applicationRole);
        }

        // GET: SiteAdmin/Roles/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.Roles.FindAsync(id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            return View(applicationRole);
        }

        // POST: SiteAdmin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Visible,System,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
        {
            if (id != applicationRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == id && !f.System);
                    if (role != null)
                    {
                        role.Visible = applicationRole.Visible;
                        role.System = applicationRole.System;
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        return Forbid();
                    }

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ApplicationRoleExists(applicationRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        Logger.LogError(ex.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationRole);
        }

        // GET: SiteAdmin/Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRole = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRole == null)
            {
                return NotFound();
            }
            else if (applicationRole.System)
            {
                return Forbid();
            }

            return View(applicationRole);
        }

        // POST: SiteAdmin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(f => f.Id == id && !f.System);
            if (role != null)
            {
                _context.Roles.Remove(role);

                await _context.SaveChangesAsync();

            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddClaims(string Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            List<RoleClaimViewModel> roleclaims = new List<RoleClaimViewModel>();
            
            foreach (var subPermission in typeof(Permissions).GetNestedTypes())
            {
                foreach (var field in subPermission.GetFields())
                {
                    string policy = field.GetValue(null).ToString();
                    var rl = await RoleManager.FindByIdAsync(Id);
                    if (rl != null)
                    {
                      
                        var cl = await RoleManager.GetClaimsAsync(rl);
                        bool active = false;
                        if (cl != null)
                        {
                            active = cl.Where(q => q.Value == policy).Any();
                        }
                        roleclaims.Add(new RoleClaimViewModel { RoleId =rl.Id, RoleName = rl.Name, Claim = policy, Active = active });
                    }
                }
            }
            return View(roleclaims);
        }

        [HttpPost, ActionName("AddClaims")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddClaimsConfirmed(List<RoleClaimViewModel> RoleClaims)
        {
            if (RoleClaims == null)
            {
                return NotFound();
            }
            ApplicationRole role = await RoleManager.FindByIdAsync(RoleClaims[0].RoleId);
            if (role != null)
            {
                foreach (var item in RoleClaims.Where(q => q.Active == true))
                {
                    // test if claim already exists, we cannot have duplicates !!!
                    if(!(await RoleManager.GetClaimsAsync(role)).Where(q => q.Value == item.Claim).Any()) 
                        await RoleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, item.Claim));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationRoleExists(string id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
