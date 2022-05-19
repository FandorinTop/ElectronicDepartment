using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.CourseTeacher.Responce
{
    public class GetCourseTeacherViewModel
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public string TeacherId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
