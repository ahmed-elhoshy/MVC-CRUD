using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_D8.Models;

namespace MVC_D8.Controllers
{
    [Authorize(Roles = "Admin,Instructor")] // either Admin or Instructor
    public class TestController : Controller
    {
        [Authorize (Roles ="Student")]  // must either be admin AND student  or  instructor AND student
        public String Display()  // cant be static or private 
        {
            return "Hello World";
        }
        [AllowAnonymous] // dont need to be authenticated
        public ViewResult ShowAdd()
        {
            return View();
        }
        //public int Add(int x, int y) // to send input in url https://localhost:7295/test/add?x=5&y=2
        //{
        //    return x + y;
        //}

        //public int Add(int x,int y,int id) ----> id=30 user input 
        //public int Add(int x, [FromQuery] int y, [FromRoute] int id) // --->  y = 1000 id= 50 action= "/test/add/50?y=1000"
        public int Add(int x, int y, int[] id, Student s1, Student s2) //id = 30,40,70,60 , s1 = (100,hamada,20)
        {
            //int x= int.Parse(Request.Form["x"]);
            //int y= int.Parse(Request.Form["y"]);
            //int id1= int.Parse(Request.Form["id"]);
            //int id2 = int.Parse(Request. RouteValues["id"].ToString());
            return x + y;
        }

        public ViewResult Show()
        {

            return View();
            //return View("renamedShow");  if i changed view name to renamedShow (convention over configuration)
            // return View("~/Views/Home/renamedShow.cshtml");  if i want to access a view in a diifrent view folder other than testController

        }

        public IActionResult Details(int id)
        {
            if (id == 0)
                return Content("Hello Mvc from details"); // text/plain    ----> didnt write id in url
            else if (id == 1)
            {
                return NotFound("Student not found"); // 404
            }
            else if (id == 2)
            {
                return RedirectToAction("Display", " ", new { x = 10, y = 20 });

                //return RedirectPermanent("https://www.google.com"); // 301 awl mra hayrooh ll server el awl wb3deen google tany mra hayrooh l google 3ala tool 

                //return Redirect("https://www.google.com"); 302
            }

            else if (id == 3)
            {
                return File("~/Names.txt", "text/plain", "alex.txt");
            }

            else
                return View(); // url: /test/details/5

        }

        public IActionResult Edit()
        {
            int id = 10;
            String name = "Omar";
            int age = 20;
            ViewData["sid"] = id;
            ViewData["sname"] = name;
            ViewBag.age = age;
            ViewBag.sid = 30; // dynamic
            Student s1 = new Student() { Id = 5, Name = "Ali", Age = 22 };
            Department d1 = new Department() { Id = 1, Name = "Pd", Capacity = 40 };
            EditStudentDepartmentViewModel editStudentDepartmentViewModel = new EditStudentDepartmentViewModel()
            {
                Student = s1,
                Department = d1
            };
            // 3ayz arg3 view shayel el student wl department fa ha3ml Viewmodel shayel el etneen

            TempData["Duration"] = 60;

            return View(editStudentDepartmentViewModel);
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Show2()
        {
            int id = 20;
            string name = "aly";
            ViewBag.id = id;
            ViewBag.name = name;
            // add cookies
            Response.Cookies.Append("id", id.ToString(), new CookieOptions() { Expires = DateTime.Now.AddMinutes(5) });
            Response.Cookies.Append("fname", name, new CookieOptions() { Expires = DateTime.Now.AddMinutes(5) });
            return View();
        }
        public IActionResult Show3()
        {

            ViewBag.id = Request.Cookies["id"].ToString();
            ViewBag.name = Request.Cookies["fname"];


            return View();
        }
        public IActionResult Show4()
        {
            int id = 20;
            string name = "aly";
            ViewBag.id = id;
            ViewBag.name = name;
            // add to server
            HttpContext.Session.SetInt32("id", id);
            HttpContext.Session.SetString("fname", name);
            return View();
        }
        public IActionResult Show5()
        {

            ViewBag.id = HttpContext.Session.GetInt32("id");
            ViewBag.name = HttpContext.Session.GetString("fname");


            return View();
        }

    }
}
