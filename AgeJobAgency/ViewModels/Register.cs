using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgeJobAgency.ViewModels
{
    public class Register 
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Text)]
        public string NRIC { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; } = string.Empty;

        [Required, MinLength(12, ErrorMessage = "Enter at least 12 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public string DateofBirth { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Resume { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string WhoamI { get; set; } = string.Empty;

    }
}