using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Services
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO)
        {
            try
            {
                HttpClient _client = _httpClientFactory.CreateClient("MangoAPI");

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDTO.Url);
                if(requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data),
                                                        Encoding.UTF8, "application/json");
                }
                switch(requestDTO.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method=HttpMethod.Get;
                        break;
                }
                HttpResponseMessage apiResponse = await _client.SendAsync(message);

                switch(apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Forbidden" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var ResponseContent = await apiResponse.Content.ReadAsStringAsync();
                        var ApiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(ResponseContent);
                        return ApiResponseDTO;
                }
            }
            catch(Exception ex) 
            {
                return new ResponseDTO
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString()
                };
            }
        }
    }
}
