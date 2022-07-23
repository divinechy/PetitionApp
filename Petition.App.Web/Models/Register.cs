using System.ComponentModel.DataAnnotations;

namespace Petition.App.Web.Models
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
        public string RoleName { get; set; }
        public int UserId { get; set; }
    }
}
