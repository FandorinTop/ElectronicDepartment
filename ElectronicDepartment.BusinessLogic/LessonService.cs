using ElectronicDepartment.Common.Exceptions;
using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Lesson;
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

        public async Task<int> CreateLesson(CreateLessonViewModel viewModel)
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
            lesson.CourseTeacherId = viewModel.CourseTeacherId;
            lesson.LessonType = viewModel.LessonType;
            lesson.GroupId = viewModel.GroupId;
        }

        public async Task UpdateLesson(UpdateLessonViewModel viewModel)
        {
            await Validate(viewModel);

            var lesson = await _context.Lessons.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(lesson, nameof(viewModel.Id), viewModel.Id.ToString());

            Map(lesson, viewModel);

            await _context.AddAsync(lesson);
            await _context.SaveChangesAsync();
        }

        private async Task Validate(BaseLessonViewModel viewModel)
        {
            await ValidateGroup(viewModel);
            await ValidateCourseTeacher(viewModel);
        }

        private async Task ValidateGroup(BaseLessonViewModel viewModel)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(item => item.Id == viewModel.GroupId);
            DbNullReferenceException.ThrowExceptionIfNull(group, nameof(viewModel.GroupId), viewModel.GroupId.ToString());
        }

        private async Task ValidateCourseTeacher(BaseLessonViewModel viewModel)
        {
            var courseTeacher = await _context.Teachers.FirstOrDefaultAsync(item => item.Id == viewModel.CourseTeacherId.ToString());
            DbNullReferenceException.ThrowExceptionIfNull(courseTeacher, nameof(viewModel.CourseTeacherId), viewModel.StudentId.ToString());
        }
    }

    public interface ILessonService
    {
        public Task<int> CreateLesson(CreateLessonViewModel viewModel);

        public Task UpdateLesson(UpdateLessonViewModel viewModel);
    }
}