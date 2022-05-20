﻿using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.Web.Shared.CourseTeacher;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(CreateCourseTeacherViewModel viewModel)
        {
            var id = await _courseTeacherService.Create(viewModel);

            return Ok(id.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IEnumerable<CreateCourseTeacherViewModel> viewModel)
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
        public async Task<IActionResult> Update(UpdateCourseTeacherViewModel viewModel)
        {
            await _courseTeacherService.Update(viewModel);

            return Ok(true);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteRange(IEnumerable<int> ids)
        {
            await _courseTeacherService.RemoveRange(ids);

            return Ok();
        }
    }
}