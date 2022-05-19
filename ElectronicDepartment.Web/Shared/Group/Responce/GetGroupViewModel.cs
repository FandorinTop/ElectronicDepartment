using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Group.Responce
{
    public class GetGroupViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public ApiResultViewModel<GetShortStudentGroupItem> Students { get; set; }
    }

    public class GetShortCourseGroupItem
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class GetShortStudentGroupItem
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(36)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(36)]
        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }
    }
}
