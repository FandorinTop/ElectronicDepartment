using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Cafedra;
using ElectronicDepartment.Web.Shared.Cafedra.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class CafedraService : ICafedraService
    {
        ApplicationDbContext _context;

        public CafedraService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CreateCafedraViewModel viewModel)
        {
            var cafedra = new Cafedra();
            Map(cafedra, viewModel);

            await _context.AddAsync(cafedra);
            await _context.SaveChangesAsync();

            return cafedra.Id;
        }

        public async Task<GetCafedraViewModel> Get(int id)
        {
            var cafedra = await _context.Cafedras.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(cafedra, nameof(id), id.ToString());

            return ExctractViewModel(cafedra);
        }

        public async Task Update(UpdateCafedraViewModel viewModel)
        {
            var cafedra = await _context.Cafedras.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(cafedra, nameof(viewModel.Id), viewModel.Id.ToString());

            Map(cafedra, viewModel);

            await _context.SaveChangesAsync();
        }

        private GetCafedraViewModel ExctractViewModel(Cafedra cafedra) => new GetCafedraViewModel()
        {
            Id = cafedra.Id,
            Name = cafedra.Name,
            Description = cafedra.Description,
            CreatedAt = cafedra.CreatedAt,
            Phone = cafedra.Phone
        };

        private void Map(Cafedra cafedra, BaseCafedraViewModel viewModel)
        {
            cafedra.Description = viewModel.Description;
            cafedra.Name = viewModel.Name;
            cafedra.Phone = viewModel.Phone;
        }
    }
}