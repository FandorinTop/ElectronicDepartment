using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.Mark;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private IMarkService _markService;

        public MarkController(IMarkService markService)
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
    }
}