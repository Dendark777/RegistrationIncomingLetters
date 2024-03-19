using Microsoft.Extensions.DependencyInjection;
using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.WPFClient.Services;

namespace RegistrationIncomingLetters.WPFClient.ViewModels
{
    public class UpdateLetterWindowViewModel : LetterWindowViewModel
    {
        #region Индекс отправителя
        private int _indexSender;

        /// <summary>
        ///  Индекс отправителя
        /// </summary>
        public int IndexSender
        {
            get => _indexSender;
            set => Set(ref _indexSender, value);
        }
        #endregion

        #region Индекс получателя
        private int _indexAddressee;

        /// <summary>
        /// Индекс получателя
        /// </summary>
        public int IndexAddressee
        {
            get => _indexAddressee;
            set => Set(ref _indexAddressee, value);
        }
        #endregion
        public void SetSelectedLetter(LetterDto letter)
        {
            Id = letter.Id;
            Name = letter.Name;
            Content = letter.Content;
            SelectedAddressee = letter.Addressee;
            SelectedSender = letter.Sender;
            IndexSender = _employees.Select(x => x.Id).ToList().IndexOf(letter.Sender.Id);
            IndexAddressee = _employees.Select(x => x.Id).ToList().IndexOf(letter.Addressee.Id);
        }
        public UpdateLetterWindowViewModel()
        {
            _sendToWeb = App.Services.GetService<LetterService>().Update;
        }
    }
}
