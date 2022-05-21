using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Interfaces;
using ElectronicDepartment.Web.Shared.Course;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel viewModel)
        {
            var id = await _courseService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var CourseViewModel = await _courseService.Get(id);

            return Ok(CourseViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetSelector(int id)
        {
            var selector = await _courseService.GetSelector();

            return Ok(selector);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourseViewModel viewModel)
        {
            await _courseService.Update(viewModel);

            return Ok(true);
        }
    }
}