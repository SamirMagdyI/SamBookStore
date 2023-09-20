using System.ComponentModel.DataAnnotations;

namespace SamBookStore.Models.Domain.ViewModels
{
    public class SignUpViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Address { get; set; }
    }
}
