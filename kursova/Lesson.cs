using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class Lesson : BaseEntity
    {
        public int GroupId { get; set; }
        
        public virtual Group Group { get; set; } = default!;

        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = default;

        public int CourseTeacherId { get; set; }

        public virtual CourseTeacher CourseTeacher { get; set; } = default!;

        public LessonType LessonType { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}