using Petition.App.Web.Models;
using Petition.App.Web.Models.APIDtos;
using Petition.App.Web.Models.APIModels;
using Petition.App.Web.Models.Dtos;

namespace Petition.App.Web.Services.IServices
{
    public interface IPetitionService
    {
        Task<ResponseDto> GetPetition(string accesstoken);//Value
        Task<ResponseDto> GetPetitionByPetitionId(int petitionId, string accesstoken);//Value
        Task<ResponseDto> GetPetitionByUserId(int userId, string accesstoken);//Value
        Task<ResponseDto> CreatePetition(PetitionDTO petition, string accesstoken);//Models.APIModels.Petition
        Task<ResponseDto> SignPetition(SignPetitionDTO petition, string accesstoken);//UserPetition
        Task DeletePetition(int petitionId, string accesstoken);
    }
}
