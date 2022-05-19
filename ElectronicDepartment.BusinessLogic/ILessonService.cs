using ElectronicDepartment.Web.Shared.Lesson;
using ElectronicDepartment.Web.Shared.Lesson.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface ILessonService
    {
        public Task<int> CreateLesson(CreateLessonViewModel viewModel);

        public Task UpdateLesson(UpdateLessonViewModel viewModel);

        public Task<GetLessonViewModel> GetLesson(int id);
    }
}