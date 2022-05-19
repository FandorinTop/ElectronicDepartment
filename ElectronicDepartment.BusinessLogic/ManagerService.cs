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
    public class ManagerService : ApplicationUserService, IUserManagerService
    {
        public const string STUDENTROLE = "student";
        public const string MANAGERROLE = "manager";
        public const string TEACHERROLE = "teacher";

        public ApplicationDbContext _context;
        public UserManager<ApplicationUser> _userManager;
        public RoleManager<IdentityRole> _roleManager;

        public ManagerService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<string> CreateStudent(CreateStudentViewModel viewModel)
        {
            var student = new Student();
            Map(student, viewModel);

            var result = await _userManager.CreateAsync(student, "a"+"A"+"!"+student.GetHashCode().ToString());

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

            Map(student, viewModel);
        }

        public Task<string> CreateTeacher(CreateTeacherViewModel viewModel)
        {
            return default;
        }

        public async Task UpdateTeacher(UpdateTeacherViewModel viewModel)
        {

        }

        public Task<string> CreateManager(CreateManagerViewModel viewModel)
        {
            return default;
        }

        public async Task UpdateManager(UpdateManagerViewModel viewModel)
        {
        }

        private void Map(Student student, BaseStudentViewModel viewModel)
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
    }
}