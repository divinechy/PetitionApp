using System;
namespace PetitionApi.DTOs
{
    public class UserLogin
    {
        public string Email { get; set;}

        public string Password { get; set; }


        public UserLogin(string Email, string Password)
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}

