﻿using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class Lesson : BaseEntity
    {
        public int CourseId { get; set; }

        public virtual Course Course { get; set; } = default;

        public int CourseTeacherId { get; set; }

        public virtual CourseTeacher CourseTeacher { get; set; } = default!;

        public virtual List<StudentOnLesson> StudentOnLessons { get; set; } = new List<StudentOnLesson>();

        public LessonType LessonType { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}