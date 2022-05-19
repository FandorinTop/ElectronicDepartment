﻿using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.Group;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupConteroller : ControllerBase
    {
        private IGroupService _groupService;

        public GroupConteroller(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGroupViewModel viewModel)
        {
            var id = await _groupService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var GroupViewModel = await _groupService.Get(id);

            return Ok(GroupViewModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGroupViewModel viewModel)
        {
            await _groupService.Update(viewModel);

            return Ok(true);
        }
    }
}