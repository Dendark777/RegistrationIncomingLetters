using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.WPFClient.ViewModels;
using System.Windows;

namespace RegistrationIncomingLetters.WPFClient.Views
{
    /// <summary>
    /// Interaction logic for UpdateLetterWindow.xaml
    /// </summary>
    public partial class UpdateLetterWindow : Window
    {
        public UpdateLetterWindow()
        {
            InitializeComponent();
        }
        public UpdateLetterWindow(LetterDto letterDto)
        {
            InitializeComponent();
            UpdateLetterWindowViewModel dataContext = (UpdateLetterWindowViewModel)DataContext;
            dataContext.SetSelectedLetter(letterDto);
        }
    }
}
