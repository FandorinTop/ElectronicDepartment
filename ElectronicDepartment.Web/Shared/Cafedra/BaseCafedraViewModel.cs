using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicDepartment.Web.Shared.Cafedra
{
    public class BaseCafedraViewModel
    {
        [Required]
        [MaxLength(64)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(600)]
        public string Description { get; set; } = default!;

        [Phone]
        [MaxLength(64)]
        public string Phone { get; set; } = default!;
    }
}
