using MVC_D8.Models;

namespace MVC_D8.Repositories
{
    public interface IDeptRepo
    {
        List<Department> GetAll();
        Department GetById(int id);
        Department Insert(Department department);
        Department Update(Department department);
        void Delete(int id);
    }
    public class DeptRepo : IDeptRepo

    {
        //ITIMvcDbContext db = new ITIMvcDbContext();
        ITIMvcDbContext db;
        public DeptRepo(ITIMvcDbContext _db) // Constructor injection
        {
            db = _db;
        }
        public List<Department> GetAll()
        {
            return db.Departments.Where(d => d.Status == true).ToList();
        }
        public Department GetById(int id) {
            return db.Departments.FirstOrDefault(d => d.Id == id);

        }
        public Department Insert(Department department) {
            db.Departments.Add(department);
            Save();
            return department;
        }
        public Department Update(Department department)
        {
            db.Departments.Update(department);
            Save();
            return department;
        }
        public void Delete(int id)
        {
            var department = db.Departments.FirstOrDefault(d => d.Id == id);
            department.Status = false; // Soft delete
            //db.Departments.Remove(department); Hard delete
            Save();
        }

        public void Save() // insted of db.SaveChanges() many times
        {   
            Console.WriteLine("Saving changes");
            db.SaveChanges();
        } 
    }
}