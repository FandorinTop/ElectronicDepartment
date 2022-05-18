using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Mark
{
    public class BaseMarkViewModel
    {
        [Required]
        public string StudentId { get; set; }

        public int LessonId { get; set; }

        [Range(0, 100)]
        public int Value { get; set; }
    }
}
