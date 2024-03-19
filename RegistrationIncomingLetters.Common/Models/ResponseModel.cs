
namespace RegistrationIncomingLetters.Common.Models
{
    public class ResponseModel<T> where T : class, new()
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public ResponseModel()
        {
            Success = true;
            ErrorMessage = "";
            Data = new T();
        }
    }
}
