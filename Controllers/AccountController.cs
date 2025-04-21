using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;

namespace MVC_D8.Controllers
{
    public class AccountController : Controller
    {
        private  ITIMvcDbContext db;  // instead we can use identity framework core

        public AccountController(ITIMvcDbContext _db)
        {

            db = _db;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.Include(u => u.Roles)
                    .FirstOrDefault(u => u.Email == model.Email && u.Name == model.UserName && u.Password==model.Password);
                //search in db
                if (user != null)
                {
                    Claim c1 = new Claim(ClaimTypes.Email, user.Email);
                    Claim c2 = new Claim(ClaimTypes.Name, user.Name);
                    Claim c3 = new Claim(ClaimTypes.Role, "Admin");
                    Claim c4 = new Claim(ClaimTypes.Role, "Student");
                    Claim c5 = new Claim(ClaimTypes.Role, "Instructor");
                    //Claim c6= new Claim(ClaimTypes.Role, user.Roles.FirstOrDefault().Name);

                    ClaimsIdentity ci = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci.AddClaim(c1);
                    ci.AddClaim(c2);
                    ci.AddClaim(c3);
                    ci.AddClaim(c4);
                    ci.AddClaim(c5);
                    ClaimsPrincipal principal = new ClaimsPrincipal(ci);
                    principal.AddIdentity(ci);
                    //sign in
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Department");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or email");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                //save to db
                User user1 = new User()
                {
                    Name = model.UserName,
                    Email = model.Email,
                    Password = model.Password
                };
                db.Users.Add(user1);
                Role r1 = db.Roles.FirstOrDefault(r => r.Name == "Admin");
                Role r2 = db.Roles.FirstOrDefault(r => r.Name == "Student");

                r1.Users.Add(user1);
                // or r1.Roles.Add(user1);
                r2.Users.Add(user1);
                db.SaveChanges(); // can do it with repo
                return RedirectToAction("Index", "Department");
            }
            return View(model);
        }
    }
}
