using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;

namespace MVC_D8.Controllers
{
    public class ManageController : Controller
    {
        private ITIMvcDbContext db { get; set; }
        public ManageController(ITIMvcDbContext _db)
        {

            db = _db;
        }


        public IActionResult AllUsers()
        {
            var model = db.Users.ToList();
            return View(model);
        }

        public IActionResult ManageRoles(int id)
        {
            var user = db.Users.Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == id);
            var rolesIn = user.Roles; // all roles of user
            var allroles = db.Roles.ToList();
            var rolesOut = allroles.Except(rolesIn).ToList(); // all roles except the ones that are already assigned to the user
            ViewBag.User = user;
            ViewBag.RolesIn = rolesIn;
            ViewBag.RolesOut = rolesOut;
            return View();
        }

        [HttpPost]
        public IActionResult ManageRoles(int id, int[] RolesToRemove,int[] RolesToAdd)
        {
            var user = db.Users.Include(u => u.Roles)
                .FirstOrDefault(u => u.Id == id);
           
            foreach (var roleID in RolesToRemove)
            {
                var r = db.Roles.FirstOrDefault(r => r.Id == roleID);
               user.Roles.Remove(r);
            }
            foreach (var roleID in RolesToAdd)
            {
                var r = db.Roles.FirstOrDefault(r => r.Id == roleID);
                user.Roles.Add(r);
            }
            // cookies still have the old roles ... sol: sign out and sign in again
            //await HttpContext.SignOutAsync();
            //await HttpContext.SignInAsync();
            
            db.SaveChanges();
            return RedirectToAction("AllUsers");
        }
    }
}
