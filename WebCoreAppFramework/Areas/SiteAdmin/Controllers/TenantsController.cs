using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCoreAppFramework.Authorization;
using WebCoreAppFramework.Data;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Areas.SiteAdmin.Controllers
{
    [Authorize(Permissions.AdminUser.Read)]
    [Area("SiteAdmin")]
    public class TenantsController
        : Controller
    {
        private readonly ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SiteAdmin/ApplicationTenants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tenants.ToListAsync());
        }

        // GET: SiteAdmin/ApplicationTenants/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationTenant == null)
            {
                return NotFound();
            }

            return View(applicationTenant);
        }

        // GET: SiteAdmin/ApplicationTenants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SiteAdmin/ApplicationTenants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LogoURL,EmailAddress,WebSite,PhoneContact,Visible,System")] ApplicationTenant applicationTenant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationTenant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationTenant);
        }

        // GET: SiteAdmin/ApplicationTenants/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTenant = await _context.Tenants.FindAsync(id);
            if (applicationTenant == null)
            {
                return NotFound();
            }
            return View(applicationTenant);
        }

        // POST: SiteAdmin/Tenants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,LogoURL,EmailAddress,WebSite,PhoneContact,Visible,System")] ApplicationTenant applicationTenant)
        {
            if (id != applicationTenant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationTenant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationTenantExists(applicationTenant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicationTenant);
        }

        // GET: SiteAdmin/Tenants/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationTenant = await _context.Tenants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationTenant == null)
            {
                return NotFound();
            }

            return View(applicationTenant);
        }

        // POST: SiteAdmin/Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var applicationTenant = await _context.Tenants.FindAsync(id);
            if (!applicationTenant.System )
            {
                _context.Tenants.Remove(applicationTenant);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationTenantExists(long id)
        {
            return _context.Tenants.Any(e => e.Id == id);
        }
    }
}
