using System;
using System.Collections.Generic;

namespace Petition.App.Web.Models.APIModels
{
    public partial class Role
    {
        public short RoleId { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
