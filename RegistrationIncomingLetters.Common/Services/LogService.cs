namespace RegistrationIncomingLetters.Common.Services
{
    public class LogService
    {
        public static void WriteLog(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
