using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.Cafedra;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CafedraConteroller : ControllerBase
    {
        private ICafedraService _cafedraService;

        public CafedraConteroller(ICafedraService cafedraService)
        {
            _cafedraService = cafedraService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCafedraViewModel viewModel)
        {
            var id = await _cafedraService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var cafedraViewModel = await _cafedraService.Get(id);

            return Ok(cafedraViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCafedraViewModel viewModel)
        {
            await _cafedraService.Update(viewModel);

            return Ok(true);
        }
    }
}