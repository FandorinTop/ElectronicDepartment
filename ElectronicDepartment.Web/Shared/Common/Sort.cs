using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.Common
{
    public class Sort
    {
        [Required]
        public string Field { get; set; } = default!;

        public SortingOrder OrderBy { get; set; } = SortingOrder.ASC;
    }
}
