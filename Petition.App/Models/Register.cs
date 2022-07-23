using System.ComponentModel.DataAnnotations;

namespace Petition.App.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
