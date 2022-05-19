using ElectronicDepartment.Web.Shared.Cafedra;
using ElectronicDepartment.Web.Shared.Cafedra.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface ICafedraService
    {
        public Task Create(CreateCafedraViewModel viewModel);

        public Task Update(UpdateCafedraViewModel viewModel);

        public Task<GetCafedraViewModel> Get(int id);
    }
}