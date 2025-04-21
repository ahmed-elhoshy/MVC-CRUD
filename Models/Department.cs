 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_D8.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }

        public bool Status { get; set; } = true;

        public virtual HashSet<Student> Students { get; set; } = new HashSet<Student>();
    }
}
