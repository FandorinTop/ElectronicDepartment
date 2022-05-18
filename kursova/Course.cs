using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.DomainEntities
{
    public class Course : BaseEntity
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        public virtual List<Lesson> GroupCourses { get; set; } = new List<Lesson>();

        public virtual List<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
    }
}