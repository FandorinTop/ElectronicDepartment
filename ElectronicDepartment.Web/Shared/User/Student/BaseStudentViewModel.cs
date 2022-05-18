using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.User.Student
{

    public class BaseStudentViewModel : BaseUserViewModel
    {
        public int GroupId { get; set; }
    }
}
