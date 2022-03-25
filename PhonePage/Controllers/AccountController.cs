using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhonePage.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PhonePage.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(MyContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        public async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(roleName: "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "Admin"));
            }
            if (!await _roleManager.RoleExistsAsync(roleName: "User"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "User"));
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IActionResult> SeedAdmin()
        {
            User user = _userManager.FindByEmailAsync("admin3@gmail.com").Result;
            if (_userManager.FindByEmailAsync("admin3@gmail.com").Result == null)
            {
                User identityUser = new User()
                {
                    UserName = "admin3",
                    Email = "admin3@gmail.com"
                };
                IdentityResult result = await _userManager.CreateAsync(identityUser, "admin33333!A");
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(identityUser, "Admin").Wait();
                    await db.SaveChangesAsync();
                    await _signInManager.SignInAsync(identityUser, true);
                    return Content("bbbbbbb");
                }

            }
            return Content("ttttttt");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View();

            if (_userManager.FindByEmailAsync(register.Email).Result != null)
            {
                ModelState.AddModelError("", "This User already Exist");
                return View();
            }
            else
            {
                User user = new User
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    UserName = register.UserName,
                    Email = register.Email
                };

                IdentityResult result = _userManager.CreateAsync(user, register.Password).Result;
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                await _userManager.AddToRoleAsync(user, "User");


                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string link = Url.Action(nameof(VerifyEmail), "Account", new { email = user.Email, token },
                    Request.Scheme, Request.Host.ToString());
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("hrmshrms2000@gmail.com");
                mail.To.Add(new MailAddress(user.Email, "Confirm Email"));
                mail.Subject = "Verify Email";
                string body = string.Empty;
                using (StreamReader reader = new StreamReader("wwwroot/templete/VerifyEmail.html"))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{{link}}", link);
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp =new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("hrmshrms2000@gmail.com", "hrms12345");
                smtp.Send(mail);
                return View(nameof(Login));


            }



        }


        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return BadRequest();
            }


            return RedirectToAction(nameof(Login), "Account");

        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                User user = _userManager.FindByEmailAsync(login.Email.ToString()).Result;
                if (await _userManager.FindByEmailAsync(login.Email) != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, true, false);
                    if (signInResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, true);
                        return RedirectToAction("Create", "Home");
                     
                    }
                    else
                    {
                        if (signInResult.IsLockedOut)
                        {
                            ModelState.AddModelError("", "Your account has beem Blocked for 5 minutes due to overtrying");
                            return View();
                        }
                        ModelState.AddModelError("", "Email or Password not Correct");
                        return View();
                    }

                }
                else
                {
                    return Content("Admin not exist");
                }
            }



        }


       


        public IActionResult Dashboard()
        {
            List<User> user = db.Users.ToList();


            return View(user);
        }


        public IActionResult Edit(string id)
        {
            ViewBag.Roles = db.Roles.ToList();
            ViewBag.UserRoles = db.UserRoles.ToList();
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }


        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(AccountVM account)
        {
            User user = await _userManager.FindByEmailAsync(account.User.Email);
            if (user == null) BadRequest();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token },
                Request.Scheme, Request.Host.ToString());

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("hrmshrms2000@gmail.com", "Reset Password");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{link}'> Plese click here to Reset Password </a>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("hrmshrms2000@gmail.com", "hrms12345");
            smtp.Send(mail);

            return RedirectToAction(nameof(Login), "Account");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            User user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();

            AccountVM account = new AccountVM
            {
                User = user,
                Token = token
            };

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
            User user = _userManager.FindByEmailAsync(account.User.Email).Result;
            if (user == null) return BadRequest();

            AccountVM model = new AccountVM
            {
                User = user,
                Token = account.Token
            };
            if (!ModelState.IsValid) return View(model);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, account.Token, account.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }


            return RedirectToAction(nameof(Login), "Account");
        }

    }
}
