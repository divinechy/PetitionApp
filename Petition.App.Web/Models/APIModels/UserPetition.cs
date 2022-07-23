﻿using System;
using System.Collections.Generic;

namespace Petition.App.Web.Models.APIModels
{
    public partial class UserPetition
    {
        public int UserPetitionId { get; set; }
        public int SignedBy { get; set; }
        public string SignedEmail { get; set; } = null!;
        public string? Remarks { get; set; }
        public DateTime? SignedDate { get; set; }
        public int PetitionId { get; set; }
    }
}
