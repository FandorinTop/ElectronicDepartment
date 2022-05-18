using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class CourseTeacher : BaseEntity
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = default!;

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; } = default!;

        public virtual List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public CourseTeacherFlag CourseTeacherFlag { get; set; }
    }
}