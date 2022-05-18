using ElectronicDepartment.Common.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.DomainEntities
{
    public class ApplizationUser : IdentityUser
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

        public Gender Gender { get; set; }
    }
}