using Microsoft.Extensions.DependencyInjection;
using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.WPFClient.Infrastructure.Commands;
using RegistrationIncomingLetters.WPFClient.Services;
using RegistrationIncomingLetters.WPFClient.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RegistrationIncomingLetters.WPFClient.ViewModels
{
    public class LetterWindowViewModel : ViewModel
    {
        protected readonly EmployeeService _employerService;
        protected Func<LetterCutDto, Task> _sendToWeb;
        protected List<EmployeeDto> _employees;
        #region Ид письма
        private ulong? _id;

        /// <summary>
        /// Ид письма
        /// </summary>
        public ulong? Id
        {
            get => _id;
            set => Set(ref _id, value);
        }
        #endregion

        #region Название
        private string _name;

        /// <summary>
        /// Название
        /// </summary>
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
        #endregion

        #region Содержание
        private string _content;

        /// <summary>
        /// Содержание
        /// </summary>
        public string Content
        {
            get => _content;
            set => Set(ref _content, value);
        }
        #endregion

        #region Выбранный отправитель
        private EmployeeDto _selectedSender;

        /// <summary>
        /// Выбранный отправитель
        /// </summary>
        public EmployeeDto SelectedSender
        {
            get => _selectedSender;
            set => Set(ref _selectedSender, value);
        }
        #endregion

        #region Отправители
        private ObservableCollection<EmployeeDto> _senders;

        /// <summary>
        /// Отправители
        /// </summary>
        public ObservableCollection<EmployeeDto> Senders
        {
            get => _senders;
            set => Set(ref _senders, value);
        }
        #endregion

        #region Выбранный получатель
        private EmployeeDto _selectedAddressee;

        /// <summary>
        /// Выбранный получатель
        /// </summary>
        public EmployeeDto SelectedAddressee
        {
            get => _selectedAddressee;
            set => Set(ref _selectedAddressee, value);
        }
        #endregion

        #region Получатели
        private ObservableCollection<EmployeeDto> _addressees;

        /// <summary>
        /// Получатели
        /// </summary>
        public ObservableCollection<EmployeeDto> Addressees
        {
            get => _addressees;
            set => Set(ref _addressees, value);
        }
        #endregion

        #region Сохранить письмо
        public ICommand SaveLetterCommand { get; private set; }
        private bool CanSaveLetterCommandExecute(object p)
        {
            bool result = SelectedAddressee != null
                          && SelectedSender != null
                          && !SelectedAddressee.Email.Equals(SelectedSender.Email);
            return result;
        }
        private void OnSaveLetterCommandExecuted(object p)
        {
            var letter = new LetterCutDto
            {
                Id = Id,
                Name = Name,
                Content = Content,
                Date = DateTime.Now,
                AddresseeId = SelectedAddressee.Id,
                SenderId = SelectedSender.Id,
            };
            Task.Run(async () =>
            {
                await _sendToWeb.Invoke(letter);
            }).Wait();
            CloseWindowCommand.Execute(p);
        }
        #endregion

        #region Закрыть окно
        public ICommand CloseWindowCommand { get; private set; }
        private void OnCloseWindowExecuted(object p)
        {
            if (p is Window window)
            {
                window.Close();
            }
        }
        #endregion


        public LetterWindowViewModel()
        {
            _employerService = App.Services.GetService<EmployeeService>();
            var result = _employerService.Get();
            _employees = result.Success ? result.Data : new List<EmployeeDto>();
            _senders = new ObservableCollection<EmployeeDto>(_employees);
            _addressees = new ObservableCollection<EmployeeDto>(_employees);

            SaveLetterCommand = new LambdaCommand(OnSaveLetterCommandExecuted, CanSaveLetterCommandExecute);
            CloseWindowCommand = new LambdaCommand(OnCloseWindowExecuted);
        }
    }
}
