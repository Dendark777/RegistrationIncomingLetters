using System.ComponentModel.DataAnnotations;

namespace RegistrationIncomingLetters.DataAccess.Models
{
    public class Letter
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int AddresseeId { get; set; }
        public virtual Employee Sender { get; set; }
        public virtual Employee Addressee { get; set; }
    }
}
