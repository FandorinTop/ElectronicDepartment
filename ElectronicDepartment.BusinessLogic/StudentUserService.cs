using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.User;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Student.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class StudentService : ApplicationUserService
    {
        ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
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

        protected override void MapApplicationUser(in ApplicationUser user, in BaseUserViewModel viewModel)
        {
            base.MapApplicationUser(user, viewModel);
            user.UserType = UserType.Student;
        }
    }

    public class ApplicationUserService
    {
        protected virtual void MapApplicationUser(in ApplicationUser user, in BaseUserViewModel viewModel)
        {
            user.PhoneNumber = viewModel.PhoneNumber;
            user.Email = viewModel.Email;
            user.BirthDay = viewModel.BirthDay;
            user.Gender = viewModel.Gender;
            user.FirstName = viewModel.FirstName;
            user.MiddleName = viewModel.MiddleName;
            user.LastName = viewModel.LastName;
        }
    }
}