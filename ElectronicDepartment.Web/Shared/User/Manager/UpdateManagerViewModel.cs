using System.ComponentModel.DataAnnotations;

namespace ElectronicDepartment.Web.Shared.User.Manager
{
    public class UpdateManagerViewModel : BaseManagerViewModel
    {
        [Required]
        public string Id { get; set; }
    }
}
