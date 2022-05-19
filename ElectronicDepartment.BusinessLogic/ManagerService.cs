﻿using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.User;
using ElectronicDepartment.Web.Shared.User.Manager;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Student.Responce;
using ElectronicDepartment.Web.Shared.User.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ElectronicDepartment.Common.Constants;

namespace ElectronicDepartment.BusinessLogic
{
    //Student
    public partial class ManagerService
    {
        public async Task<string> CreateStudent(CreateStudentViewModel viewModel)
        {
            var student = new Student();
            Map(student, viewModel);

            var result = await _userManager.CreateAsync(student, "a" + "A" + "!" + student.GetHashCode().ToString());

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(student, STUDENTROLE);
            }

            await _context.SaveChangesAsync();

            return student.Id;
        }

        public async Task UpdateStudent(UpdateStudentViewModel viewModel)
        {
            var student = await _context.Students.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(student, nameof(viewModel.Id), viewModel.Id);

            await ValidateGroup(viewModel);

            Map(student, viewModel);

            await _context.SaveChangesAsync();
        }

        public async Task<GetStudentViewModel> Get(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(student, nameof(id), id);

            return ExtractViewModel(student);
        }

        private async Task ValidateGroup(BaseStudentViewModel viewModel)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(item => item.Id == viewModel.GroupId);
            DbNullReferenceException.ThrowExceptionIfNull(group, nameof(viewModel.GroupId), viewModel.GroupId.ToString());
        }

        private void Map(Student student, BaseStudentViewModel viewModel)
        {
            MapApplicationUser(student, viewModel);
            student.UserType = UserType.Teacher;
            student.GroupId = viewModel.GroupId;
        }

        private GetStudentViewModel ExtractViewModel(Student student) => new GetStudentViewModel()
        {
            Id = student.Id,
            FirstName = student.FirstName,
            MiddleName = student.MiddleName,
            LastName = student.LastName,
            BirthDay = student.BirthDay,
            Email = student.Email,
            Gender = student.Gender,
            GroupId = student.GroupId,
            PhoneNumber = student.PhoneNumber
        };
    }

    //Teacher
    public partial class ManagerService
    {
        public async Task<string> CreateTeacher(CreateTeacherViewModel viewModel)
        {
            await ValidateCafedra(viewModel);

            var teacher = new Teacher();
            Map(teacher, viewModel);

            var result = await _userManager.CreateAsync(teacher, "a" + "A" + "!" + teacher.GetHashCode().ToString());

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(teacher, TEACHERROLE);
            }

            return default;
        }

        public async Task UpdateTeacher(UpdateTeacherViewModel viewModel)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(teacher, nameof(viewModel.Id), viewModel.Id.ToString());

            await ValidateCafedra(viewModel);

            Map(teacher, viewModel);

            await _context.SaveChangesAsync();
        }

        private async Task ValidateCafedra(BaseTeacherViewModel viewModel)
        {
            var cafedra = await _context.Cafedras.FirstOrDefaultAsync(item => item.Id == viewModel.CafedraId);
            DbNullReferenceException.ThrowExceptionIfNull(cafedra, nameof(viewModel.CafedraId), viewModel.CafedraId.ToString());
        }

        private void Map(Teacher teacher, BaseTeacherViewModel viewModel)
        {
            MapApplicationUser(teacher, viewModel);
            teacher.UserType = UserType.Teacher;
            teacher.CafedraId = viewModel.CafedraId;
            teacher.AcademicAcredition = viewModel.AcademicAcredition;
        }
    }

    //Manager
    public partial class ManagerService
    {
        public async Task<string> CreateManager(CreateManagerViewModel viewModel)
        {
            var manager = new Manager();
            Map(manager, viewModel);

            var result = await _userManager.CreateAsync(manager, "a" + "A" + "!" + manager.GetHashCode().ToString());

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(manager, MANAGERROLE);
            }

            return manager.Id;
        }

        public async Task UpdateManager(UpdateManagerViewModel viewModel)
        {
            var manager = await _context.Managers.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(manager, nameof(viewModel.Id), viewModel.Id.ToString());

            Map(manager, viewModel);

            await _context.SaveChangesAsync();
        }

        private void Map(Manager manager, BaseManagerViewModel viewModel)
        {
            MapApplicationUser(manager, viewModel);
            manager.UserType = UserType.Manager;
        }
    }

    public partial class ManagerService : ApplicationUserService, IUserManagerService
    {
        

        public ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public ManagerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}