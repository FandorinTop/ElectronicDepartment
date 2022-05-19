using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.User;
using ElectronicDepartment.Web.Shared.User.Student.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class StudentService
    {
        ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetStudentViewModel> Get(string id)
        {
            var user = await _context.Students.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(user, nameof(user.Id), id);

            return ExtractViewModel(user);
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

        protected static void MapApplicationUser(ApplizationUser user, BaseUserViewModel viewModel)
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