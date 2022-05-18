using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.User.Teacher
{
    public class BaseTeacherViewModel : BaseUserViewModel
    {
        public AcademicAcredition AcademicAcredition { get; set; }
    }
}
