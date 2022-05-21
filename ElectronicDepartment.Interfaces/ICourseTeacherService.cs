using ElectronicDepartment.Web.Shared.CourseTeacher;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;

namespace ElectronicDepartment.Interfaces
{
    public interface ICourseTeacherService
    {
        public Task<int> Create(CreateCourseTeacherViewModel viewModel);

        public Task<IEnumerable<int>> CreateRange(IEnumerable<CreateCourseTeacherViewModel> viewModel);

        public Task Update(UpdateCourseTeacherViewModel viewModel);

        public Task<GetCourseTeacherViewModel> Get(int id);

        public Task RemoveTeacher(int id);

        public Task RemoveTeacherRange(IEnumerable<int> ids);

        public Task<IEnumerable<GetCourseTeacherSelectorViewModel>> GetSelector();

    }
}