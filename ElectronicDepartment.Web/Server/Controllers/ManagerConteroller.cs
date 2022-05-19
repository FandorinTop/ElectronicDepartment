using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.User.Student;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerConteroller : ControllerBase
    {
        private IUserManagerService _managerService;

        public ManagerConteroller(IUserManagerService ManagerService)
        {
            _managerService = ManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel viewModel)
        {
            var id = await _managerService.CreateStudent(viewModel);

            return Ok(id.ToString());
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var ManagerViewModel = await _managerService.GetStudent(id);

        //    return Ok(ManagerViewModel);
        //}

        //[HttpPut]
        //public async Task<IActionResult> Update(UpdateManagerViewModel viewModel)
        //{
        //    await _managerService.UpdateStudent(viewModel);

        //    return Ok(true);
        //}
    }
}