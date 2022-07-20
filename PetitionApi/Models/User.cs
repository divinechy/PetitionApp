using System;
using System.Collections.Generic;

namespace PetitionApi.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string LastName { get; set; } = null!;
        public string? FirstName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public short RoleId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
