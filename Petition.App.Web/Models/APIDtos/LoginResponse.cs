using System;
namespace Petition.App.Web.Models.APIDtos
{
    public class LoginResponse
    {
        public int UserId { get; set; }

        public string LastName { get; set; } = null!;

        public string? FirstName { get; set; }

        public string Email { get; set; } = null!;

        public short RoleId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Token { get; set; } = null!;
       
    }
}

