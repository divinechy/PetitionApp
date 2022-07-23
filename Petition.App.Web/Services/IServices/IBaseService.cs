using Petition.App.Web.Models;
using Petition.App.Web.Models.Dtos;

namespace Petition.App.Web.Services.IServices
{
    public interface IBaseService : IDisposable
    {
        ResponseDto response { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}
