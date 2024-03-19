using System.Net.Http;

namespace RegistrationIncomingLetters.WPFClient.Services
{
    public abstract class BaseService
    {
        protected readonly HttpClient _client;
        protected string _url;
        protected BaseService()
        {
            string baseAddress = Properties.Settings.Default.BaseWebApiUrl;
            _client = new()
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

    }
}
