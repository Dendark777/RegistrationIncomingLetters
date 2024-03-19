namespace RegistrationIncomingLetters.Common.Models.Dto
{
    public class LetterDto : LetterCutDto
    {
        public EmployeeDto Sender { get; set; }
        public EmployeeDto Addressee { get; set; }

    }
}
