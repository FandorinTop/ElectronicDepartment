using ElectronicDepartment.Web.Shared.CourseTeacher;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface ICourseTeacherService
    {
        public Task<int> Create(CreateCourseTeacherViewModel viewModel);
        
        public Task Update(UpdateCourseTeacherViewModel viewModel);

        public Task<GetCourseTeacherViewModel> Get(int id);
    }
}