using ElectronicDepartment.Web.Shared.Mark;
using ElectronicDepartment.Web.Shared.Mark.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface IMarkService
    {
        public Task<int> Create(CreateMarkViewModel viewModel);

        public Task Update(UpdateMarkViewModel viewModel);

        public Task<GetMarkResponce> Get(int id);

        public Task GetAllMark();
    }
}