using Microsoft.Extensions.DependencyInjection;
using RegistrationIncomingLetters.WPFClient.Services;
using System.Windows;

namespace RegistrationIncomingLetters.WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private static IServiceProvider _services;
        public static IServiceProvider Services => _services ??= Initializeservices().BuildServiceProvider();

        private static IServiceCollection Initializeservices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<LetterService>();
            services.AddSingleton<EmployeeService>();

            return services;
        }
    }
}