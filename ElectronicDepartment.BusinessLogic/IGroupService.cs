﻿using ElectronicDepartment.Web.Shared.Group;
using ElectronicDepartment.Web.Shared.Group.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public interface IGroupService
    {
        public Task<GetGroupViewModel> Get(int id);

        public Task<int> Create(CreateGroupViewModel viewModel);

        public Task Update(UpdateGroupViewModel viewModel);

        public Task<IEnumerable<GetGroupSelectorViewModel>> GetSelector();
    }
}