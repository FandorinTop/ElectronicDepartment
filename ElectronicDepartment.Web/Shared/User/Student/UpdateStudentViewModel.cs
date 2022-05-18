using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.User.Student
{
    public class UpdateStudentViewModel : BaseStudentViewModel
    {
        [Required]
        public string Id { get; set; } = default!;
    }
}
