using Newtonsoft.Json;
using RegistrationIncomingLetters.Common.Models;
using RegistrationIncomingLetters.Common.Services;
using System.Net.Http;

namespace RegistrationIncomingLetters.WPFClient.Services
{
    public abstract class GetDtosService<TDto> : BaseService where TDto : class
    {
        public ResponseModel<List<TDto>> Get()
        {
            HttpResponseMessage response = _client.GetAsync(_url).Result;
            ResponseModel<List<TDto>> result = new ResponseModel<List<TDto>>();
            Task.Run(async () =>
            {
                result = await ProcessingHttpResponseMessage<List<TDto>>(response);
            }).Wait();
            return result;
        }

        protected async Task<ResponseModel<T>> ProcessingHttpResponseMessage<T>(HttpResponseMessage response)
            where T : class, new()
        {
            ResponseModel<T> result;
            try
            {
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<ResponseModel<T>>(responseBody);
            }
            catch (Exception ex)
            {
                result = new ResponseModel<T>();
                result.Success = false;
                result.ErrorMessage = ex.Message;
                LogService.WriteLog(ex);
            }

            return result;
        }
    }
}
