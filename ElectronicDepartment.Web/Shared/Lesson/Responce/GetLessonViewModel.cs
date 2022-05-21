using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Lesson.Responce
{
    public class GetLessonViewModel
    {
        public int Id { get; set; }

        public int? CourseId { get; set; }

        public string CourseName { get; set; }

        public string CourseTeacher { get; set; }
        
        public int? CourseTeacherId { get; set; }

        public LessonType LessonType { get; set; }

        public DateTime LessonStart { get; set; }

        public int Duration { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
