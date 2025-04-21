using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;
using MVC_D8.Repositories;

namespace MVC_D8.Controllers
{
    public class DepartmentController : Controller
    {
        //ITIMvcDbContext db = new ITIMvcDbContext();
        //DeptRepo deptRepo= new DeptRepo();
        IDeptRepo deptRepo;
        public DepartmentController(IDeptRepo _deptRepo) // Constructor injection
        {
            deptRepo = _deptRepo;
        }

        public IActionResult testHashCode([FromServices] IDeptRepo deptr) 
        {
            return Content($"{deptRepo.GetHashCode()} - {deptr.GetHashCode()}");
        }


        public IActionResult Index() 
        {
            var departments = deptRepo.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            var departments = deptRepo.GetAll();
            ViewBag.Departments = departments;

            return View();

        }
        [HttpPost] // Action selector
        public IActionResult Create(Department department) // go here if request is post
        {
            if (ModelState.IsValid)
            {
                deptRepo.Insert(department);
                //var students = db.Students.Include(s => s.Department).ToList();
                return RedirectToAction("Index");
            }


            return RedirectToAction("index");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }


            Department d = deptRepo.GetById(id.Value);
            if (d == null)
            {
                return NotFound();
            }
            return View(d);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //Department d = db.Departments.FirstOrDefault(d => d.Id == id);
            Department d =deptRepo.GetById(id.Value);

            if (d == null)
            {
                return NotFound();
            }
            ViewBag.Departments = deptRepo.GetAll();
            return View(d);
        }

        [HttpPost]
        public IActionResult Edit(Department newDepartment)
        {
            //db.Update(newDepartment);
            //db.SaveChanges();
            deptRepo.Update(newDepartment);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Department d = deptRepo.GetById(id);
            if (d == null)
            {
                return NotFound();
            }

            //db.Departments.Remove(d);
            //db.SaveChanges();
            deptRepo.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
