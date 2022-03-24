using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Controllers
{
    public class UserController : Controller
    {
        private readonly MyContext db;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(MyContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            List<User> user = db.Users.ToList();
            ViewBag.Roles = db.Roles.ToList();
            return View(user);

       
        }
        public async Task<IActionResult>  Update(string id)
        {            
            User user = db.Users.FirstOrDefault(i => i.Id == id);
            IList<string> userrole = await userManager.GetRolesAsync(user);
            ViewBag.Roles =await db.Roles.ToListAsync();
            ViewBag.UserRoles = await userManager.GetRolesAsync(user);
            return View(user);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(User userr)
        {
            User user = db.Users.FirstOrDefault(i => i.Id == userr.Id);
            if (user == null) return NotFound();

            ViewBag.Roles = await db.Roles.ToListAsync();
            ViewBag.UserRoles = await userManager.GetRolesAsync(user);
            //if (userr.RoleId == "---" && userr.newRoleId=="---")
            //{
            //    return RedirectToAction(nameof(Index), "User");
            //}
            if(userr.newRoleId != "---")
            {
                IdentityResult result = await userManager.AddToRoleAsync(user, userr.newRoleId);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(userr);
            }
            }
            if (userr.RoleId != "---")
            {

                IdentityResult result = await userManager.RemoveFromRoleAsync(user, userr.RoleId);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(userr);
                }
            }
          



            return RedirectToAction(nameof(Index), "User");


        }

        //public async  DeleteRole(User userr)
        //{
        //    User user = db.Users.FirstOrDefault(i => i.Id == userr.Id);
        //    if (db.Roles.Any(i => i.Name == userr.RoleId))
        //    {
        //        IdentityResult result = await userManager.RemoveFromRoleAsync(user, userr.RoleId);
        //        if (!result.Succeeded)
        //        {
        //            foreach (var item in result.Errors)
        //            {
        //                ModelState.AddModelError("", item.Description);
        //            }
        //        }
        //    }
        //}



        //    [HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteRole(User userr)
        //{
        //    User user = db.Users.FirstOrDefault(i => i.Id == userr.Id);
        //    if (user == null) return NotFound();

        //    ViewBag.Roles = await db.Roles.ToListAsync();
        //    ViewBag.UserRoles = await userManager.GetRolesAsync(user);
           
        //    if (userr.RoleId == "---")
        //        return NotFound();
        //    IdentityResult result = await userManager.RemoveFromRoleAsync(user, userr.RoleId);
        //    if (!result.Succeeded)
        //    {
        //        foreach (var item in result.Errors)
        //        {
        //            ModelState.AddModelError("", item.Description);
        //        }
        //        return View(userr);
        //    }

            


        //    return RedirectToAction(nameof(Index), "User");


        //}


        public IActionResult Delete(string id)
        {

            User user = db.Users.FirstOrDefault(i => i.Id == id);
            if (user == null) return BadRequest();
            db.UserRoles.Remove(db.UserRoles.FirstOrDefault(i => i.UserId == id));
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction(nameof(Index), "User");


        }
    }
}
