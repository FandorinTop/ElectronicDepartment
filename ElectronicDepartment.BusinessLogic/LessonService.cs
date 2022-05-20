using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Lesson;
using ElectronicDepartment.Web.Shared.Lesson.Responce;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.BusinessLogic
{
    public class LessonService : ILessonService
    {
        ApplicationDbContext _context;

        public LessonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(CreateLessonViewModel viewModel)
        {
            await Validate(viewModel);

            var lesson = new Lesson();
            Map(lesson, viewModel);

            await _context.AddAsync(lesson);
            await _context.SaveChangesAsync();

            return lesson.Id;
        }

        private void Map(Lesson lesson, BaseLessonViewModel viewModel)
        {
            lesson.CourseId = viewModel.CourseId;
            lesson.CourseTeacherId = viewModel.CourseTeacherId;
            lesson.LessonType = viewModel.LessonType;
            lesson.LessonStart = viewModel.LessonStart;
            lesson.Duration = viewModel.Duration;
        }

        public async Task Update(UpdateLessonViewModel viewModel)
        {
            await Validate(viewModel);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(lesson, nameof(viewModel.Id), viewModel.Id.ToString());

            Map(lesson, viewModel);

            await _context.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

        public async Task<GetLessonViewModel> Get(int id)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(lesson, nameof(id), id.ToString());

            return ExtractViewModel(lesson);
        }

        private GetLessonViewModel ExtractViewModel(Lesson item) => new GetLessonViewModel()
        {
            Id = item.Id,
            CourseId = item.CourseId,
            CourseTeacherId = item.CourseTeacherId,
            LessonType = item.LessonType,
            CreatedAt = item.CreatedAt,
            LessonStart = item.LessonStart,
            Duration = item.Duration
        };

        private async Task Validate(BaseLessonViewModel viewModel)
        {
            await ValidateCourse(viewModel);
            await ValidateCourseTeacher(viewModel);
        }

        private async Task ValidateCourseTeacher(BaseLessonViewModel viewModel)
        {
            var courseTeacher = await _context.CourseTeachers.FirstOrDefaultAsync(item => item.Id == viewModel.CourseTeacherId);
            DbNullReferenceException.ThrowExceptionIfNull(courseTeacher, nameof(viewModel.CourseTeacherId), viewModel.CourseTeacherId.ToString());
        }

        private async Task ValidateCourse(BaseLessonViewModel viewModel)
        {
            var course = await _context.CourseTeachers.FirstOrDefaultAsync(item => item.Id == viewModel.CourseId);
            DbNullReferenceException.ThrowExceptionIfNull(course, nameof(viewModel.CourseId), viewModel.CourseId.ToString());
        }
    }
}