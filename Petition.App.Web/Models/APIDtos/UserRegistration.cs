using System;
namespace Petition.App.Web.Models.APIDtos
{
    public class UserRegistration
    {

        public string LastName { get; set; }

        public string? FirstName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserRegistration(string LastName, string Email, string Password)
        {
            this.LastName = LastName;
            this.Email = Email;
            this.Password = Password;
        }
    }
}

