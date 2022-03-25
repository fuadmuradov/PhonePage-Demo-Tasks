using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Authorization;

namespace PhonePage.Controllers
{
    public class Lstorage : Controller
    {
        private readonly MyContext db;
        private readonly UserManager<User> userManager;

        public Lstorage(MyContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Basket()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Basket( string username, List<Product> products)
        {
            if (products == null)
            {
                return Json(new
                {
                    status = 400
                });

            }
        
            User user = await userManager.FindByNameAsync(username);
            await db.Products.AddRangeAsync(products);
              await db.SaveChangesAsync();
            foreach (var item in products)
            {
                UserToProduct userToProduct = new UserToProduct() {
                    UserId =  user.Id,
                    ProductId = db.Products.FirstOrDefault(x => x.productid == item.productid).id
                };
                await db.UserToProducts.AddAsync(userToProduct);
            }

            await db.SaveChangesAsync();
           
            return Json(new
            {
                status = 200
            });

        }
    }
}
