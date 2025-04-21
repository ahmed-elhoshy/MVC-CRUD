namespace MVC_D8.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public List<User> Users { get; set; } = new List<User>();
    }
}
