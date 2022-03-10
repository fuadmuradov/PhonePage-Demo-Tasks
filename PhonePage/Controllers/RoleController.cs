using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Controllers
{
    public class RoleController : Controller
    {
        private MyContext db;
        private RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> _roleManager, MyContext _db)
        {
            roleManager = _roleManager;
            db = _db;
        }
        public IActionResult Index()
        {

            return View(roleManager.Roles);
        }

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Role");
                }    
            }
            return View();
        }


        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole identityRole = await roleManager.FindByIdAsync(id);
            IdentityResult result = await roleManager.DeleteAsync(identityRole);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                return RedirectToAction("Index", "Role");
            }

            return RedirectToAction("Index", "Role");
        }


        public async Task<IActionResult> Update(string id)
        {
            IdentityRole identityRole = await roleManager.FindByIdAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }

            return View(identityRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(IdentityRole identityRole)
        {
           // IdentityResult result = await roleManager.UpdateAsync(identityRole);
            IdentityRole identity = db.Roles.FirstOrDefault(i => i.Id == identityRole.Id);
            identity.Name = identityRole.Name;
            identity.NormalizedName = identityRole.Name.ToUpper();
             await db.SaveChangesAsync();
           

            return RedirectToAction("Index", "Role");
        }

    }
}
