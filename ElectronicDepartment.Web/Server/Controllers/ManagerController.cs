using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.User.Manager;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private IUserManagerService _managerService;

        public ManagerController(IUserManagerService ManagerService)
        {
            _managerService = ManagerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentViewModel viewModel)
        {
            var id = await _managerService.CreateStudent(viewModel);

            return Ok(id.ToString());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentViewModel viewModel)
        {
            await _managerService.UpdateStudent(viewModel);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(CreateManagerViewModel viewModel)
        {
            var id = await _managerService.CreateManager(viewModel);

            return Ok(id.ToString());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateManager(UpdateManagerViewModel viewModel)
        {
            await _managerService.UpdateManager(viewModel);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacher(CreateTeacherViewModel viewModel)
        {
            var id = await _managerService.CreateTeacher(viewModel);

            return Ok(id.ToString());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacher(UpdateTeacherViewModel viewModel)
        {
            await _managerService.UpdateTeacher(viewModel);

            return Ok();
        }
    }
}