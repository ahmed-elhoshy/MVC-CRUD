using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace MVC_D8.Models
{
    public class Student
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "*"), StringLength(20, MinimumLength = 3, ErrorMessage = "Please enter a string of max 20 characters and min of 3 characters")]
        public string Name { get; set; }


        [Range(20, 30, ErrorMessage = "Please enter a valid age between 20 and 30")]
        public int Age { get; set; }


        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"), Required(ErrorMessage = " please enter a valid email")]
        [Remote("CheckEmailExistance", "students", AdditionalFields = "Name , Age")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*"), StringLength(20, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Compare("Password")]
        [NotMapped]
        [DataType(DataType.Password)]
        public String Cpassword { get; set; }



        [ForeignKey("Department")] // Department stands for the navigation property in the Student class
        public int? DeptNo { get; set; }  // nullable int as if department deleted then student will not be deleted their deptid = Null 


        public Department? Department { get; set; }


        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }

    }
}
