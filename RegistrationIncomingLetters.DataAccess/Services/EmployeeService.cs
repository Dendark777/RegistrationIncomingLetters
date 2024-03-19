using RegistrationIncomingLetters.Common.Models.Dto;
using RegistrationIncomingLetters.DataAccess.Data;
using RegistrationIncomingLetters.DataAccess.Models;

namespace RegistrationIncomingLetters.DataAccess.Services
{
    public class EmployeeService : BaseService<Employee, EmployeeDto>
    {
        public EmployeeService(AppDbContext context) : base(context)
        {
        }
    }
}
