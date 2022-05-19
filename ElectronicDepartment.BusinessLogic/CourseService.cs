using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Course;
using ElectronicDepartment.Web.Shared.Course.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class CourseService : ICourseService
    {
        ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CreateCourseViewModel viewModel)
        {
            var course = new Course();
            MapCourse(course, viewModel);

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return course.Id;
        }

        public async Task Update(UpdateCourseViewModel viewModel)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(course, nameof(viewModel.Id), viewModel.Id.ToString());

            MapCourse(course, viewModel);

            await _context.SaveChangesAsync();
        }

        public async Task<GetCourseViewModel> Get(int id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(course, nameof(id), id.ToString());

            return ExtractViewModel(course);
        }

        private void MapCourse(Course course, BaseCourseViewModel viewModel)
        {
            course.Name = viewModel.Name;
            course.Description = viewModel.Description;
        }

        private GetCourseViewModel ExtractViewModel(Course item) => new GetCourseViewModel()
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            CreatedAt = item.CreatedAt
        };
    }
}