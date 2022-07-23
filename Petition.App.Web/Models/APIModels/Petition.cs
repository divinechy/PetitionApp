using System;
using System.Collections.Generic;

namespace Petition.App.Web.Models.APIModels
{
    public partial class Petition
    {
        public int PetitionId { get; set; }
        public int CreatedBy { get; set; }
        public string Name { get; set; } = null!;
        public string? Details { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
