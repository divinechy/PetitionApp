using Newtonsoft.Json;
using Petition.App.Web.Models;
using Petition.App.Web.Models.Dtos;
using Petition.App.Web.Services.IServices;
using System.Net.Http.Headers;
using System.Text;

namespace Petition.App.Web.Services
{
    public class BaseService : IBaseService
    {
        public ResponseDto response { get; set; }
        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClient)
        {
            this.httpClient = httpClient;
            this.response = new ResponseDto();
        }
        public async Task<T> SendAsync<T>(ApiRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("PetitionAPI");
                HttpRequestMessage msg = new HttpRequestMessage() { Version = new Version(2, 0) };
                msg.Headers.Add("accept", "application/json");
                msg.RequestUri = new Uri(apiRequest.Url);
                client.DefaultRequestHeaders.Clear();
                if (apiRequest.Data != null)
                {
                    msg.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }

                if (!string.IsNullOrEmpty(apiRequest.AccessToken))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.AccessToken);
                }
                HttpResponseMessage apiResponse = null;
                switch (apiRequest.ApiType)
                {
                    case SD.ApiType.GET:
                        msg.Method = HttpMethod.Get;
                        break;
                    case SD.ApiType.POST:
                        msg.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        msg.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        msg.Method = HttpMethod.Delete;
                        break;
                    default:
                        msg.Method = HttpMethod.Get;
                        break;
                }
                apiResponse = await client.SendAsync(msg);

                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var apiResponseDto = JsonConvert.DeserializeObject<T>(apiContent);
                return apiResponseDto;
            }
            catch (Exception e)
            {
                var dto = new ResponseDto
                {
                    DisplayMessage = "Error",
                    ErrorMessages = new List<string> { Convert.ToString(e.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apiResponseDto = JsonConvert.DeserializeObject<T>(res);
                return apiResponseDto;
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
