using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Group
{
    public class BaseGroupViewModel
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;
    }
}
