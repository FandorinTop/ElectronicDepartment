using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Lesson
{
    public class BaseLessonViewModel
    {
        public int CourseTeacherId { get; set; }

        public LessonType LessonType { get; set; }

        public TimeSpan TimeSpan { get; set; }
    }
}
