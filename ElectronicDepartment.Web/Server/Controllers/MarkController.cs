using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.Mark;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private IStudentOnLessonService _markService;

        public MarkController(IStudentOnLessonService markService)
        {
            _markService = markService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMarkViewModel viewModel)
        {
            var id = await _markService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var MarkViewModel = await _markService.Get(id);

            return Ok(MarkViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMarkViewModel viewModel)
        {
            await _markService.Update(viewModel);

            return Ok(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentLesson()
        {
            var responce = await _markService.GetStudentSelector();

            return Ok(responce);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentOnLesson(int id)
        {
            var responce = await _markService.GetStudentsWithMarkViewModel(id);

            return Ok(responce);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _markService.Remove(id);

            return Ok();
        }
    }
}