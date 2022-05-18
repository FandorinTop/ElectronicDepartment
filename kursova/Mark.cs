using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.DomainEntities
{
    public class Mark : BaseEntity
    {
        public int CurrentGroupId { get; set; }
        public virtual Lesson GroupCourse { get; set; } = default!;

        public int StudentId { get; set; }

        public virtual Student Student { get; set; } = default!;
        
        public int CourseTeacherId { get; set; }

        public virtual CourseTeacher CourseTeacher { get; set; } = default!;

        [Range(0, 100)]
        public int Value { get; set; }
    }
}