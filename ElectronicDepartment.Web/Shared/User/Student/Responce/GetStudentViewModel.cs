using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.User.Student.Responce
{
    public class GetStudentViewModel
    {
        public string Id { get; set; }

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

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public int GroupId { get; set; }
    }
}
