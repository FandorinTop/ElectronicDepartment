using ElectronicDepartment.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.User
{
    public class BaseUserViewModel
    {
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; } = default!;

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; } = default!;

        [Required]
        [MaxLength(128)]
        public string MiddleName { get; set; } = default!;

        public DateTime BirthDay { get; set; }

        public Gender Gender { get; set; } = Gender.None;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
