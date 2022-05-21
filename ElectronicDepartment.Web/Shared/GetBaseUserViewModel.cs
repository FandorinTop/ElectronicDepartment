using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.Web.Shared
{
    public class GetBaseUserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string MiddleName { get; set; } = default!;

        public DateTime BirthDay { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
