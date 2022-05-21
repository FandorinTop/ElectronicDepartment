using ElectronicDepartment.Web.Shared.Lesson;
using ElectronicDepartment.Web.Shared.Lesson.Responce;

namespace ElectronicDepartment.Interfaces
{
    public interface ILessonService
    {
        public Task<int> Create(CreateLessonViewModel viewModel);

        public Task Update(UpdateLessonViewModel viewModel);

        public Task<GetLessonViewModel> Get(int id);

        public Task<IEnumerable<GetCourseLessonViewModel>> GetCourseLessons(int courseId);
    }
}