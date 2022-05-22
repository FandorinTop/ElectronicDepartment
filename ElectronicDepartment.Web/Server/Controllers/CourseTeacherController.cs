using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.CourseTeacher;
using Microsoft.AspNetCore.Mvc;
using ElectronicDepartment.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseTeacherController : ControllerBase
    {
        private ICourseTeacherService _courseTeacherService;

        public CourseTeacherController(ICourseTeacherService courseTeacherService)
        {
            _courseTeacherService = courseTeacherService;
        }

        [HttpPost]
        [Authorize(Roles = "admin, manager, teacher")]
        public async Task<IActionResult> Create(CreateCourseTeacherViewModel viewModel)
        {
            var id = await _courseTeacherService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRange(IEnumerable<CreateCourseTeacherViewModel> viewModel)
        {
            var ids = await _courseTeacherService.CreateRange(viewModel);

            return Ok(ids);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var CourseTeacherViewModel = await _courseTeacherService.Get(id);

            return Ok(CourseTeacherViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSelector()
        {
            var selector = await _courseTeacherService.GetSelector();

            return Ok(selector);
        }

        [HttpPut]
        [Authorize(Roles = "admin, manager, teacher")]
        public async Task<IActionResult> Update(UpdateCourseTeacherViewModel viewModel)
        {
            await _courseTeacherService.Update(viewModel);

            return Ok(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int courseId, int teacherId)
        {
            //TODO FIX
            //_courseTeacherService.Remove();

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRange(IEnumerable<KeyValuePair<int, int>> ids)
        {
            //await _courseTeacherService.RemoveRange(ids);

            return Ok();
        }
    }
}