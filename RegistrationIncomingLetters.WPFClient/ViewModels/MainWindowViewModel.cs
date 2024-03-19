using Microsoft.Extensions.DependencyInjection;
using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.WPFClient.Infrastructure.Commands;
using RegistrationIncomingLetters.WPFClient.Services;
using RegistrationIncomingLetters.WPFClient.ViewModels.Base;
using RegistrationIncomingLetters.WPFClient.Views;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RegistrationIncomingLetters.WPFClient.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private LetterService _letterService;

        #region Выделенное письмо
        private LetterDto _selectedLetter;

        /// <summary>
        /// Письма
        /// </summary>
        public LetterDto SelectedLetter
        {
            get => _selectedLetter;
            set => Set(ref _selectedLetter, value);
        }
        #endregion

        #region Письма
        private ObservableCollection<LetterDto> _letters;

        /// <summary>
        /// Письма
        /// </summary>
        public ObservableCollection<LetterDto> Letters
        {
            get => _letters;
            set => Set(ref _letters, value);
        }
        #endregion

        #region Закрыть приложение
        public ICommand CloseApplicationCommand { get; private set; }
        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion

        #region Открыть окно регистрации письма
        public ICommand OpenCreateLetterDialogCommand { get; private set; }
        private void OnOpenCreateLetterDialogExecuted(object p)
        {
            var createLetterDialog = new CreateLetterWindow();
            createLetterDialog.ShowDialog();
            GetLetters();
            MessageBox.Show("Сохранилось");
        }
        #endregion

        #region Подтвердить удаление письма
        public ICommand OpenDeleteLetterDialogCommand { get; private set; }
        private bool CanOpenDeleteLetterDialogCommandExecute(object p)
        {
            bool result = SelectedLetter != null;
            return result;
        }
        private void OnOpenDeleteLetterDialogExecuted(object p)
        {
            if (MessageBox.Show($"Вы уверены что хотите удалить письмо {SelectedLetter.Name}?",
                    "Предупреждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Task.Run(async () =>
                {
                    await _letterService.Delete(SelectedLetter.Id);
                    SelectedLetter = null;
                    GetLetters();

                }).Wait();
                MessageBox.Show("Сохранилось");
            }

        }
        #endregion

        #region Открыть окно редактирования письма
        public ICommand OpenUpdateLetterDialogCommand { get; private set; }
        private bool CanOpenUpdateLetterDialogCommandExecute(object p)
        {
            bool result = SelectedLetter != null;
            return result;
        }
        private void OnOpenUpdateLetterDialogExecuted(object p)
        {
            var letterDialog = new UpdateLetterWindow(SelectedLetter);
            letterDialog.ShowDialog();
            GetLetters();
            MessageBox.Show("Сохранилось");
        }
        #endregion

        #region Обновить список писем
        public ICommand RefreshLetterListCommand { get; private set; }
        private void OnRefreshLetterListExecuted(object p)
        {
            GetLetters();
            MessageBox.Show("Обновлено");
        }
        #endregion


        public MainWindowViewModel()
        {
            _letterService = App.Services.GetService<LetterService>();
            GetLetters();


            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted);
            OpenCreateLetterDialogCommand = new LambdaCommand(OnOpenCreateLetterDialogExecuted);
            OpenUpdateLetterDialogCommand = new LambdaCommand(OnOpenUpdateLetterDialogExecuted, CanOpenUpdateLetterDialogCommandExecute);
            OpenDeleteLetterDialogCommand = new LambdaCommand(OnOpenDeleteLetterDialogExecuted, CanOpenDeleteLetterDialogCommandExecute);
            RefreshLetterListCommand = new LambdaCommand(OnRefreshLetterListExecuted);
        }

        private void GetLetters()
        {
            var result = _letterService.Get();
            Letters = new ObservableCollection<LetterDto>(result.Success ? result.Data : new List<LetterDto>());
        }
    }
}
