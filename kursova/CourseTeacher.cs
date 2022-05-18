using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class CourseTeacher : BaseEntity
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = default!;

        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; } = default!;

        public CourseTeacherFlag CourseTeacherFlag { get; set; }
    }
}