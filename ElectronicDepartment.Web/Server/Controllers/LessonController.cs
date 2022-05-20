using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.Lesson;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonViewModel viewModel)
        {
            var id = await _lessonService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var LessonViewModel = await _lessonService.Get(id);

            return Ok(LessonViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLessonViewModel viewModel)
        {
            await _lessonService.Update(viewModel);

            return Ok(true);
        }
    }
}