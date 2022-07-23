using Petition.App.Web.Models;
using Petition.App.Web.Models.APIDtos;
using Petition.App.Web.Models.APIModels;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services.IServices;

namespace Petition.App.Web.Services
{
    public class PetitionService : BaseService, IPetitionService
    {
        public ResponseDto response { get; set; }
        private IHttpClientFactory _clientFactory;
        public PetitionService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ResponseDto> GetPetition(string accesstoken)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.PetitionAPIBase}/api/petition/getpetitions",
                AccessToken = accesstoken
            });
        }

        public async Task<ResponseDto> GetPetitionByPetitionId(int petitionId, string accesstoken)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.PetitionAPIBase}/api/petition/getpetitionbypetitionid/" + petitionId,
                AccessToken = accesstoken
            });
        }

        public async Task<ResponseDto> GetPetitionByUserId(int userId, string accesstoken)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.PetitionAPIBase}/api/petition/getpetitionbyuserid/" + userId,
                AccessToken = accesstoken
            });
        }

        public async Task<ResponseDto> CreatePetition(PetitionDTO petition, string accesstoken)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = petition,
                Url = $"{SD.PetitionAPIBase}/api/petition/createpetition",
                AccessToken = accesstoken
            });
        }

        public async Task<ResponseDto> SignPetition(SignPetitionDTO petition, string accesstoken)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = petition,
                Url = $"{SD.PetitionAPIBase}/api/petition/signpetition",
                AccessToken = accesstoken
            });
        }

        public async Task DeletePetition(int petitionId, string accesstoken)
        {
             await this.SendAsync<PetitionVM>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.PetitionAPIBase}/api/petition/deletepetition/" + petitionId,
                AccessToken = accesstoken
            });
        }
    }
}
