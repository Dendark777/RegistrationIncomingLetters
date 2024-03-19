using Microsoft.Extensions.DependencyInjection;
using RegistrationIncomingLetters.WPFClient.Services;

namespace RegistrationIncomingLetters.WPFClient.ViewModels
{
    public class CreateLetterWindowViewModel : LetterWindowViewModel
    {

        public CreateLetterWindowViewModel()
        {
            _sendToWeb = App.Services.GetService<LetterService>().Create;
        }
    }
}
