using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.Course
{
    public class UpdateCourseViewModel : BaseCourseViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
