using Petition.App.Web.Models;
using Petition.App.Web.Models.APIDtos;
using Petition.App.Web.Models.APIModels;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services.IServices;

namespace Petition.App.Web.Services
{
    public class UserService : BaseService, IUserService
    {
        public ResponseDto response { get; set; }
        private IHttpClientFactory _clientFactory;
        public UserService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<ResponseDto> CreateUser(UserRegistration registration)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = registration,
                Url = $"{SD.PetitionAPIBase}/api/user/createuser",
            });
        }

        public async Task<ResponseDto> GetUserByEmail(string email)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.PetitionAPIBase}/api/user/getuserbyemail/" + email,
            });
        }

        public async Task<ResponseDto> GetUsers()
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.PetitionAPIBase}/api/user/getusers"
            });
        }

        public async Task<ResponseDto> Login(UserLogin user)
        {
            return await this.SendAsync<ResponseDto>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = user,
                Url = $"{SD.PetitionAPIBase}/api/user/login",
            });
        }
    }
}
