using Microsoft.EntityFrameworkCore;
using MVC_D8.Models;

namespace MVC_D8.Repositories
{
    public interface IStudentRepo
    {
        List<Student> GetAll();
        Student GetById(int id);
        Student Insert(Student student);
        Student Update(Student student);
        void Delete(int id);
    }

    public class StudentRepo : IStudentRepo
    {
        //ITIMvcDbContext db = new ITIMvcDbContext();
        ITIMvcDbContext db;
        public StudentRepo(ITIMvcDbContext _db) // Constructor injection
        {
            db = _db;
        }
        public List<Student> GetAll()
        {
            return db.Students.Include(s => s.Department).ToList();
        }
        public Student GetById(int id)
        {
            return db.Students.Include(s=>s.Department).FirstOrDefault(s => s.Id == id);
        }
        public Student Insert(Student student)
        {
            db.Students.Add(student);
            Save();
            return student;
        }
        public Student Update(Student student)
        {
            db.Students.Update(student);
            Save();
            return student;
        }
        public void Delete(int id)
        {
            var student = db.Students.FirstOrDefault(s => s.Id == id);
           
            db.Students.Remove(student); //Hard delete
            Save();
        }
        public void Save() // insted of db.SaveChanges() many times
        {
            Console.WriteLine("Saving changes");
            db.SaveChanges();
        }
    }
}
