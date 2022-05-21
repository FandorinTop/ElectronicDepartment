using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Mark.Responce
{
    public class GetMarkResponce
    {
        public int Id { get; set; }

        public string StudentId { get; set; }

        public int LessonId { get; set; }

        public int? Value { get; set; }

        public DateTime CreatedAt { get; set; }
    }

}
