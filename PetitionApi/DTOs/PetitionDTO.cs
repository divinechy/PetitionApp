using System;
namespace PetitionApi.DTOs
{
    public class PetitionDTO
    {
        public int CreatedBy { get; set; }

        public string Name { get; set; } = null!;

        public string? Details { get; set; }
    }
}

