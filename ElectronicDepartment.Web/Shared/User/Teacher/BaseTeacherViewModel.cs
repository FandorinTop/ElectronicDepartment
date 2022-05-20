using ElectronicDepartment.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.User.Teacher
{
    public class BaseTeacherViewModel : BaseUserViewModel
    {
        [Required]
        public int CafedraId { get; set; }

        public AcademicAcredition AcademicAcredition { get; set; } = AcademicAcredition.None;
    }
}
