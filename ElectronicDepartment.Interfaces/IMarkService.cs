using ElectronicDepartment.Web.Shared.Mark;
using ElectronicDepartment.Web.Shared.Mark.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface IStudentOnLessonService
    {
        public Task<int> Create(CreateMarkViewModel viewModel);

        public Task Update(UpdateMarkViewModel viewModel);

        public Task<GetMarkResponce> Get(int id);

        public Task GetAllMark();

        public Task Remove(int id);

        public Task<IEnumerable<GetStudentSelectViewModel>> GetStudentSelector();

        public Task<IEnumerable<GetStudentOnTheLessonViewModel>> GetStudentsWithMarkViewModel(int id);
    }
}