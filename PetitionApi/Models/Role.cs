using System;
using System.Collections.Generic;

namespace PetitionApi.Models
{
    public partial class Role
    {
        public short RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
