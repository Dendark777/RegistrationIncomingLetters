using RegistrationIncomingLetters.Common.Models.Dto;

namespace RegistrationIncomingLetters.WPFClient.Services
{
    public class EmployeeService : GetDtosService<EmployeeDto>
    {

        public EmployeeService()
        {
            _url = "api/employee";
        }

    }
}
