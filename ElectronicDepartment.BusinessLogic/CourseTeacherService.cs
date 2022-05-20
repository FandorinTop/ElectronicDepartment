using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.CourseTeacher;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class CourseTeacherService : ICourseTeacherService
    {
        ApplicationDbContext _context;

        public CourseTeacherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CreateCourseTeacherViewModel viewModel)
        {
            await Validate(viewModel);

            var courseTeacher = new CourseTeacher();
            Map(courseTeacher, viewModel);

            await _context.AddAsync(courseTeacher);
            await _context.SaveChangesAsync();

            return courseTeacher.Id;
        }

        private async Task Validate(BaseCourseTeacherViewModel viewModel)
        {
            await ValidateTeacher(viewModel);
            await ValidateCourse(viewModel);
        }

        private async Task ValidateTeacher(BaseCourseTeacherViewModel viewModel)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(item => item.Id == viewModel.TeacherId.ToString());
            DbNullReferenceException.ThrowExceptionIfNull(teacher, nameof(viewModel.TeacherId), viewModel.TeacherId.ToString());
        }

        private async Task ValidateCourse(BaseCourseTeacherViewModel viewModel)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == viewModel.CourseId);
            DbNullReferenceException.ThrowExceptionIfNull(course, nameof(viewModel.CourseId), viewModel.CourseId.ToString());
        }

        public async Task Update(UpdateCourseTeacherViewModel viewModel)
        {
            var courseTeacher = await _context.CourseTeachers.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(courseTeacher, nameof(viewModel.Id), viewModel.Id.ToString());
            await Validate(viewModel);
            
            Map(courseTeacher, viewModel);

            await _context.SaveChangesAsync();
        }

        public async Task<GetCourseTeacherViewModel> Get(int id)
        {
            var courseTeacher = await _context.CourseTeachers.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(courseTeacher, nameof(id), id.ToString());

            return ExtractViewModel(courseTeacher);
        }

        private GetCourseTeacherViewModel ExtractViewModel(CourseTeacher item) => new GetCourseTeacherViewModel()
        {
            Id = item.Id,
            CourseId = item.CourseId,
            TeacherId = item.TeacherId,
            CreatedAt = item.CreatedAt
        };

        private void Map(CourseTeacher courseTeacher, BaseCourseTeacherViewModel viewModel)
        {
            courseTeacher.CourseId = viewModel.CourseId;
            courseTeacher.TeacherId = viewModel.TeacherId;
        }

        public async Task<IEnumerable<GetCourseTeacherSelectorViewModel>> GetSelector()
        {
            var responce = await _context.CourseTeachers
                            .Where(item => item.DeletedAt == DateTime.MinValue)
                            .Select(item => new GetCourseTeacherSelectorViewModel
                            {
                                Id = item.Id,
                                FirstName = item.Teacher.FirstName,
                                LastName = item.Teacher.LastName,
                                MiddleName = item.Teacher.MiddleName,
                                AcademicAcredition = item.Teacher.AcademicAcredition,
                                CourseId = item.CourseId
                            }).ToListAsync();

            return responce;
        }
    }
}