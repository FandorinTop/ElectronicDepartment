﻿using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Interfaces;
using ElectronicDepartment.Web.Shared.Group;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicDepartment.Web.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService _groupService;

        public GroupController(IGroupService groupService)
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
            var groupViewModel = await _groupService.Get(id);

            return Ok(groupViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentSelector()
        {
            var selector = await _groupService.GetStudentSelector();

            return Ok(selector);
        }

        [HttpGet]
        public async Task<IActionResult> GetSelector()
        {
            var selector = await _groupService.GetSelector();

            return Ok(selector);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateGroupViewModel viewModel)
        {
            await _groupService.Update(viewModel);

            return Ok();
        }
    }
}