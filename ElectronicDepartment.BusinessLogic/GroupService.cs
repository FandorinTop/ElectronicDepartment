using ElectronicDepartment.BusinessLogic.Helpers;
using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Group;
using ElectronicDepartment.Web.Shared.Group.Responce;
using Microsoft.EntityFrameworkCore;
using static ElectronicDepartment.BusinessLogic.Helpers.ApiResponceHelper;

namespace ElectronicDepartment.BusinessLogic
{
    public class GroupService : IGroupService
    {
        ApplicationDbContext _context;

        public GroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateGroup(CreateGroupViewModel viewModel)
        {
            var group = new Group();
            Map(group, viewModel);

            await _context.Groups.AddAsync(group);
            await _context.SaveChangesAsync();

            return group.Id;
        }

        public async Task<GetGroupViewModel> GetGroup(int id)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(group, nameof(id), id.ToString());

            var dataQuery = _context
                .Students
                .AsQueryable()
                .Where(student => student.GroupId == group.Id);

            var students = await dataQuery.CreateApiResponceAsync((item) => new GetShortStudentGroupItem()
            {
                Id = item.Id,
                BirthDay = item.BirthDay,
                FirstName = item.FirstName,
                LastName = item.LastName,
                MiddleName = item.MiddleName,
            });

            var result = ExtractViewModel(group);
            result.Students = students;

            return result;
        }

        public async Task UpdateGroup(UpdateGroupViewModel viewModel)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(group, nameof(viewModel.Id), viewModel.Id.ToString());

            Map(group, viewModel);

            await _context.SaveChangesAsync();
        }

        private void Map(Group group, BaseGroupViewModel viewModel)
        {
            group.Name = viewModel.Name;
        }

        private GetGroupViewModel ExtractViewModel(Group group)
        {
            return new GetGroupViewModel()
            {
                Id = group.Id,
                Name = group.Name,
                CreatedAt = group.CreatedAt,
            };
        }
    }

    public interface IGroupService
    {
        public Task<GetGroupViewModel> GetGroup(int id);

        public Task<int> CreateGroup(CreateGroupViewModel viewModel);

        public Task UpdateGroup(UpdateGroupViewModel viewModel);
    }
}