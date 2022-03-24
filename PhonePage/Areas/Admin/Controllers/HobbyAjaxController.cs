using Microsoft.AspNetCore.Mvc;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HobbyAjaxController : Controller
    {
        private readonly MyContext db;

        public HobbyAjaxController(MyContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            List<Hobby> hobbies = db.Hobbies.ToList();
            return View(hobbies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Create(string name)
        {
            if (name == null)
            {
                return Json(new
                {
                    status = 400
                });

             }

            Hobby hobby = new Hobby()
            {
                Name = name
            };

            db.Hobbies.Add(hobby);
            await db.SaveChangesAsync();

            return Json(new
            {
                status = 200
            });

        }

    }
}
