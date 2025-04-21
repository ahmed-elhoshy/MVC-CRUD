using System.ComponentModel.DataAnnotations;

namespace MVC_D8.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
      
    }
}
