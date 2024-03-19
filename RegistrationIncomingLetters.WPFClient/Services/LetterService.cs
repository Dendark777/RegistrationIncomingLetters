using Newtonsoft.Json;
using RegistrationIncomingLetters.Common.Models;
using RegistrationIncomingLetters.Common.Models.Dto;
using System.Net.Http;

namespace RegistrationIncomingLetters.WPFClient.Services
{
    public class LetterService : GetDtosService<LetterDto>
    {
        public LetterService()
        {
            _url = "api/Letter";
        }

        public async Task<ResponseModel<LetterDto>> Create(LetterCutDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);

            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync(_url, httpContent);
            return await ProcessingHttpResponseMessage<LetterDto>(response);
        }
        public async Task<ResponseModel<LetterDto>> Update(LetterCutDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PutAsync(_url, httpContent);
            return await ProcessingHttpResponseMessage<LetterDto>(response);
        }
        public async Task<ResponseModel<LetterDto>> Delete(ulong? id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{_url}?id={id}");
            return await ProcessingHttpResponseMessage<LetterDto>(response);
        }
    }
}
