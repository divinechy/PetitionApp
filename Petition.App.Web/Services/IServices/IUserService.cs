using Petition.App.Web.Models.APIDtos;
using Petition.App.Web.Models.APIModels;
using Petition.App.Web.Models.Dtos;

namespace Petition.App.Web.Services.IServices
{
    public interface IUserService
    {
        Task<ResponseDto> Login(UserLogin user);//LoginResponse
        Task<ResponseDto> GetUsers();//IEnumerable<User>
        Task<ResponseDto> GetUserByEmail(string email);//User
        Task<ResponseDto> CreateUser(UserRegistration registration);//User
    }
}
