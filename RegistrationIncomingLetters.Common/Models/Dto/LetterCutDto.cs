using System.ComponentModel.DataAnnotations;

namespace RegistrationIncomingLetters.Common.Models.Dto
{
    public class LetterCutDto
    {
        public ulong? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int AddresseeId { get; set; }
    }
}
