﻿using System;
namespace PetitionApi.DTOs
{
    public class SignPetitionDTO
    {
        public int SignedBy { get; set; }

        public string SignedEmail { get; set; } = null!;

        public string? Remarks { get; set; }

        public int PetitionId { get; set; }
    }
}

