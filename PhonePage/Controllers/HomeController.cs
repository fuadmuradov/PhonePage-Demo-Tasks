using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhonePage.Extension;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PhonePage.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly MyContext db;
        private readonly IWebHostEnvironment env;

        public HomeController(MyContext context, IWebHostEnvironment _env)
        {
            db = context;
            env = _env;
        }

        public IActionResult Index(int ? id)
        {
           Specification spec = db.Specifications.Find(id);
            return View(spec);
        }

        public IActionResult Privacy()
        {
            return View(db.Specifications.ToList());
        }

        public IActionResult Create()
        {
            Specification spec = new Specification();
            return View(spec);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Specification specification)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!specification.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Image format does not right!");
                return View(specification);
            }

            string folder = @"images\";
            specification.Image = await specification.Photo.SavaAsync(env.WebRootPath, folder);

             await db.Specifications.AddAsync(specification);
             await db.SaveChangesAsync();






            return RedirectToAction(nameof(Privacy));
        }

    }
}
