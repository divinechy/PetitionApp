using System;
using PetitionApi.DTOs;
using PetitionApi.Models;

namespace PetitionApi.Interfaces
{
    public interface IPetitionRepository
    {
        public object GetPetitions();

        public object GetPetitionsByPetitionId(int PetitionId);

        public object GetPetitionsByUserId(int UserId);

        public Petition CreatePetition(PetitionDTO petitionDTO);

        public UserPetition? SignPetition(SignPetitionDTO signDTO);

        public void DeletePetition(int PetitionId);
    }
}

