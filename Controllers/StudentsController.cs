using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;
using MVC_D8.Repositories;

namespace MVC_D8.Controllers
{
    public class StudentsController : Controller
    {
        //url : /Students/Index
        //ITIMvcDbContext db = new ITIMvcDbContext();
        IDeptRepo deptRepo; // = new DeptRepo();
        IStudentRepo studentRepo; // = new StudentRepo();


        public StudentsController(IDeptRepo _deptRepo, IStudentRepo _studentRepo) // Constructor injection
        {
            deptRepo = _deptRepo;
            studentRepo = _studentRepo;
        }
        public IActionResult Index() // used to display the list of students
        {
            var students = studentRepo.GetAll();
            return View(students);
        }

        public IActionResult Create()
        {
            var departments = deptRepo.GetAll();
            ViewBag.Departments = departments;

            return View();

        }
        [HttpPost] // Action selector
        public IActionResult Create(Student student) // go here if request is post
        {
            if (ModelState.IsValid)
            {
               studentRepo.Insert(student);
                //var students = db.Students.Include(s => s.Department).ToList();
                return RedirectToAction("Index");
            }
            var departments =deptRepo.GetAll();
            ViewBag.Departments = departments;


            return View (student);
        }
        public IActionResult CheckEmailExistance(string email, string Name, int Age)
        {
            var student = studentRepo.GetAll().FirstOrDefault(s => s.Email == email);
            if (student == null)
            {
                return Json(true);
            }
            var nameParts = Name.Split(' ');
            var suggestedEmail = string.Join("", nameParts).ToLower() + Age + "@gmail.com";
            return Json($"Email already exists, you can use {suggestedEmail}");
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }


            //Student s = db.Students.Include(s => s.Department).FirstOrDefault(s => s.Id == id);
            Student s = studentRepo.GetById(id.Value); // lazm .value 3shan el param nullable
            if (s == null)
            {
                return NotFound();
            }
            return View(s);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }


            Student s = studentRepo.GetById(id.Value);
            if (s == null)
            {
                return NotFound();
            }
            ViewBag.Departments = deptRepo.GetAll();
            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(Student newStudent)
        {
           studentRepo.Update(newStudent);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std =  studentRepo.GetById(id.Value);
            //var std = db.Students.FirstOrDefault(s => s.Id == id);
            //or
            //db.Students.Where(s => s.Id == id).ExecuteDelete();
            if (std == null)
            {
                return NotFound();
            }

            studentRepo.Delete(id.Value);
            return RedirectToAction("Index");
        }
    }
}
