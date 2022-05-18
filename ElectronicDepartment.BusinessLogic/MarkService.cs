using ElectronicDepartment.DataAccess;
using ElectronicDepartment.DomainEntities;
using ElectronicDepartment.Web.Shared.Mark;
using ElectronicDepartment.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using ElectronicDepartment.Web.Shared.Mark.Responce;

namespace ElectronicDepartment.BusinessLogic
{
    public class MarkService : IMarkService
    {
        ApplicationDbContext _context;

        public MarkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddMark(CreateMarkViewModel viewModel)
        {
            await Validate(viewModel);

            var mark = new Mark();
            Map(mark, viewModel);

            await _context.Marks.AddAsync(mark);
            await _context.SaveChangesAsync();

            return mark.Id;
        }

        public async Task UpdateMark(UpdateMarkViewModel viewModel)
        {
            var mark = await _context.Marks.FirstOrDefaultAsync(item => item.Id == viewModel.Id);
            DbNullReferenceException.ThrowExceptionIfNull(mark, nameof(viewModel.Id), viewModel.Id.ToString());
            
            await Validate(viewModel);
            Map(mark, viewModel);

            await _context.SaveChangesAsync();
        }

        public async Task<GetMarkResponce> GetMark(int id)
        {
            var mark = await _context.Marks.FirstOrDefaultAsync(item => item.Id == id);
            DbNullReferenceException.ThrowExceptionIfNull(mark, nameof(id), id.ToString());

            return ExtractMarkResponce(mark);
        }

        public Task GetAllMark()
        {
            throw new NotImplementedException();
        }

        private GetMarkResponce ExtractMarkResponce(Mark item) => new GetMarkResponce()
        {
            Id = item.Id,
            StudentId = item.StudentId,
            LessonId = item.LessonId,
            CreatedAt = item.CreatedAt,
            Value = item.Value
        };

        private async Task Validate(BaseMarkViewModel viewModel)
        {
            await ValidateLesson(viewModel);
            await ValidateStudent(viewModel);
        }

        private async Task ValidateLesson(BaseMarkViewModel viewModel)
        {
            var lesson = await _context.Lessons.FirstOrDefaultAsync(item => item.Id == viewModel.LessonId);
            DbNullReferenceException.ThrowExceptionIfNull(lesson, nameof(viewModel.LessonId), viewModel.LessonId.ToString());
        }

        private async Task ValidateStudent(BaseMarkViewModel viewModel)
        {
            var student = await _context.Students.FirstOrDefaultAsync(item => item.Id == viewModel.StudentId);
            DbNullReferenceException.ThrowExceptionIfNull(student, nameof(viewModel.StudentId), viewModel.StudentId.ToString());
        }

        private void Map(Mark mark, BaseMarkViewModel viewModel)
        {
            mark.Value = viewModel.Value;
            mark.StudentId = viewModel.StudentId;
            mark.LessonId = viewModel.LessonId;
        }
    }
}