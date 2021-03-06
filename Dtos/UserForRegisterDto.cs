using System.ComponentModel.DataAnnotations;

namespace OOAD.Dtos
{
    public class UserForRegisterDto
    {
        [Required] public string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 16 characters")]
        public string Password { get; set; }
        [Required] public string StudentId { get; set; }
        [Required] public string Firstname { get; set; }
        [Required] public string Lastname { get; set; }
        [Required] public string Faculty { get; set; }
        [Required] public string Telephon { get; set; }
        [Required] public string Email { get; set; }


    }
}