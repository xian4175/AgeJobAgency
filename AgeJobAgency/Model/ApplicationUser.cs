using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AgeJobAgency.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public string NRIC { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
        public string DateofBirth { get; set; } = string.Empty;
        public string WhoamI { get; set;} = string.Empty;
        public string? Resume { get; set; }

    }
}
