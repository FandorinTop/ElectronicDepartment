using ElectronicDepartment.Web.Shared.User.Manager;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Teacher;

namespace ElectronicDepartment.BusinessLogic
{
    public interface IUserManagerService
    {
        public Task<string> CreateStudent(CreateStudentViewModel viewModel);

        public Task UpdateStudent(UpdateStudentViewModel viewModel);

        public Task<string> CreateTeacher(CreateTeacherViewModel viewModel);

        public Task UpdateTeacher(UpdateTeacherViewModel viewModel);

        public Task<string> CreateManager(CreateManagerViewModel viewModel);

        public Task UpdateManager(UpdateManagerViewModel viewModel);
    }
}