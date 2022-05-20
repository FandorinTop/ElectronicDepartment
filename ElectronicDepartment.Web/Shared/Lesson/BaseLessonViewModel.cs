using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Lesson
{
    public class BaseLessonViewModel
    {
        public int CourseId { get; set; }

        public int CourseTeacherId { get; set; }

        public LessonType LessonType { get; set; }

        public DateTime LessonStart { get; set; }

        //Minutes
        [Range(0, 360)]
        public int Duration { get; set; }
    }
}
