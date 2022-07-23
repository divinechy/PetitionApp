using System;
namespace Petition.App.Web.Models.APIDtos
{ 
    public class PetitionDTO
    {
        public int CreatedBy { get; set; }

        public string Name { get; set; } = null!;

        public string? Details { get; set; }
    }
}

