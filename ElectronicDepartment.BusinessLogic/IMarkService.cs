using ElectronicDepartment.Web.Shared.Mark;
using ElectronicDepartment.Web.Shared.Mark.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface IMarkService
    {
        public Task<int> AddMark(CreateMarkViewModel viewModel);

        public Task UpdateMark(UpdateMarkViewModel viewModel);

        public Task<GetMarkResponce> GetMark(int id);

        public Task GetAllMark();
    }
}