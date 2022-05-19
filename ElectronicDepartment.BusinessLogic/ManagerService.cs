using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.User;
using ElectronicDepartment.Web.Shared.User.Manager;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Student.Responce;
using ElectronicDepartment.Web.Shared.User.Teacher;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class ManagerService : ApplicationUserService, IManagerService
    {
        public ApplicationDbContext _context;
        public UserManager<ApplicationUser> userManager;
        public RoleManager<ApplicationUser> roleManager;

        public ManagerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationUser> roleManager)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<GetStudentViewModel> Get(string id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(student, nameof(id), id);

            return ExtractViewModel(student);
        }

        public async Task Update(UpdateStudentViewModel viewModel)
        {
            var student = await _context.Students.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(student, nameof(viewModel.Id), viewModel.Id);

            Map(student, viewModel);
        }

        private void Map(Student student, UpdateStudentViewModel viewModel)
        {
            MapApplicationUser(student, viewModel);
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

        public Task<string> CreateStudent(CreateStudentViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudent(UpdateStudentViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTeacher(CreateTeacherViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTeacher(UpdateTeacherViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateManager(CreateManagerViewModel viewModel)
        {
            throw new NotImplementedException();
        }

        public Task UpdateManager(UpdateManagerViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}