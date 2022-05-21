using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.CourseTeacher.Responce
{
    public class GetCourseTeacherSelectorViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public AcademicAcredition AcademicAcredition { get; set; }
    }
}
