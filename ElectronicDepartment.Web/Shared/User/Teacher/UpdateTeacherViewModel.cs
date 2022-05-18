using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.User.Teacher
{
    public class UpdateTeacherViewModel : BaseTeacherViewModel
    {
        [Required]
        public string Id { get; set; } = default!;
    }
}
