using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Client.Services;
using ElectronicDepartment.Web.Shared.Course.Responce;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;
using ElectronicDepartment.Web.Shared.Group.Responce;
using ElectronicDepartment.Web.Shared.Lesson;
using ElectronicDepartment.Web.Shared.Mark;
using ElectronicDepartment.Web.Shared.Mark.Responce;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Lesson
    {
        private int _index = -1;
        private bool _isDelete = false;
        private bool _isStudentOnLessonEdit = false;
        private bool dialogIsOpen = false;
        private bool snackBar = false;

        private List<GetStudentSelectViewModel> FreeStudentSelectors { get; set; } = new List<GetStudentSelectViewModel>();
        private List<GetStudentOnTheLessonViewModel> StudentInLessons { get; set; } = new List<GetStudentOnTheLessonViewModel>();

        private UpdateLessonViewModel Model { get; set; } = new UpdateLessonViewModel();

        private UpdateMarkViewModel StudentOnLesson { get; set; } = new UpdateMarkViewModel();

        private string Title { get; set; } = string.Empty;

        [Inject]
        IHttpService HttpClient { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public int? Id { get; set; }

        private LessonType[] LessonTypes = new LessonType[]
        {
            LessonType.None,
            LessonType.Practic,
            LessonType.Lab,
            LessonType.Lecture,
            LessonType.Exam,
        };

        private async Task CloseDialog()
        {
            dialogIsOpen = false;

            if (!_isStudentOnLessonEdit)
            {
                await AddStudentOnLessonAsync(StudentOnLesson);
            }
            else
            {
                await UpdateStudentOnLessonAsync(StudentOnLesson);

                var selected = StudentInLessons.FirstOrDefault(item => item.Id == StudentOnLesson.Id);

                if (selected is not null)
                {
                    selected.Mark = StudentOnLesson.Value;
                }
            }
        }

        private async Task DeleteLessonAsync()
        {
            _isDelete = true;

            if (StudentOnLesson != null && StudentOnLesson.Id > 0)
            {
                var studentOnLesson = StudentInLessons.FirstOrDefault(item => item.Id == StudentOnLesson.Id);

                if (studentOnLesson is null)
                {
                    return;
                }
                else
                {
                    await RemoveFromLessonAsync(studentOnLesson);
                    _isDelete = false;

                    StateHasChanged();
                }
            }
        }

        private void OpenDialog(bool isEdit)
        {
            _isStudentOnLessonEdit = isEdit;

            if (!_isStudentOnLessonEdit)
            {
                StudentOnLesson = new UpdateMarkViewModel();
            }

            dialogIsOpen = true;
        }


        public async Task SelectionChangedEvent(object obj)
        {
            var lesson = obj as GetStudentOnTheLessonViewModel;

            if (lesson is null)
            {
                StudentOnLesson = new UpdateMarkViewModel();
            }
            else
            {
                StudentOnLesson = new UpdateMarkViewModel()
                {
                    Id = lesson.Id,
                    Value = lesson.Mark,
                    LessonId = Id.Value,
                    StudentId = lesson.StudentId
                };

                if (_isDelete)
                {
                    await RemoveFromLessonAsync(lesson);

                    StateHasChanged();
                }
            }
        }

        private string CustomStringSelectorHandler(GetStudentSelectViewModel item)
        {
            StudentOnLesson.StudentId = item.Id;

            return item.FullName;
        }


        public async Task Success()
        {
            Console.WriteLine("Success");

            await UpdateAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAsync();

            Title = Id == null ? "Create " + nameof(Student) : "Edit " + nameof(Student) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (Id != null)
            {
                var responce = await HttpClient.GetAsync($"api/Lesson/Get?id={Id}");
                var result = await responce.Content.ReadFromJsonAsync<UpdateLessonViewModel>();

                await GetStudentAsync();
                await GetStudentOnLesson();
                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task GetStudentAsync()
        {
            var responce = await HttpClient.GetAsync($"api/Mark/GetStudentLesson");
            var studentValue = await responce.Content.ReadFromJsonAsync<IEnumerable<GetStudentSelectViewModel>>();

            if (studentValue != null)
            {
                var removed = studentValue
                    .Where(item => !item.LessonIds.Contains(Id.Value))
                    .ToList();

                FreeStudentSelectors.AddRange(removed);
            }
        }

        private async Task GetStudentOnLesson()
        {
            var responce = await HttpClient.GetAsync($"api/mark/GetStudentOnLesson?id={Id}");
            var studentValue = await responce.Content.ReadFromJsonAsync<List<GetStudentOnTheLessonViewModel>>();

            StudentInLessons = studentValue ?? new List<GetStudentOnTheLessonViewModel>();
        }

        #region Requests

        private async Task UpdateStudentOnLessonAsync(UpdateMarkViewModel addViewModel)
        {
            var result = await HttpClient.PutAsync(@"api/mark/update", addViewModel);

            if (result.IsSuccessStatusCode)
            {
                await JS.InvokeAsync<object>("alert", $"Successful updated student on lesson");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task AddStudentOnLessonAsync(UpdateMarkViewModel addViewModel)
        {
            addViewModel.LessonId = Id.Value;
            var result = await HttpClient.PostAsync(@"api/mark/create", addViewModel);

            if (result.IsSuccessStatusCode)
            {
                var lessonId = Convert.ToInt32(await result.Content.ReadAsStringAsync());

                var student = FreeStudentSelectors.FirstOrDefault(item => item.Id == addViewModel.StudentId);

                if(student is not null)
                {
                    var studentOnLessonItem = new GetStudentOnTheLessonViewModel();
                    Map(studentOnLessonItem, student);

                    studentOnLessonItem.Id = lessonId;
                    studentOnLessonItem.Mark = addViewModel.Value;

                    StudentInLessons.Add(studentOnLessonItem);
                    FreeStudentSelectors.Remove(student);
                }

                await JS.InvokeAsync<object>("alert", $"Successful added student on lesson");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task RemoveFromLessonAsync(GetStudentOnTheLessonViewModel removeViewModel)
        {
            var result = await HttpClient.DeleteAsync(@$"api/mark/delete?id={removeViewModel.Id}");

            if (result.IsSuccessStatusCode)
            {
                var studentInLesson = StudentInLessons.FirstOrDefault(item => item.StudentId == removeViewModel.StudentId);

                if (studentInLesson is not null)
                {
                    var freeStudent = new GetStudentSelectViewModel();
                    Map(freeStudent, studentInLesson);

                    StudentInLessons.Remove(studentInLesson);
                    FreeStudentSelectors.Add(freeStudent);

                    await JS.InvokeAsync<object>("alert", $"Successful removed student from lesson");
                }
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }
        #endregion
        
        private void Map(GetStudentOnTheLessonViewModel lesson, GetStudentSelectViewModel freeStudent)
        {
            lesson.StudentFullName = freeStudent.FullName;
            lesson.StudentGroupName = freeStudent.GroupName;
            lesson.StudentGroupId = freeStudent.GroupId;
            lesson.StudentId = freeStudent.Id;
        }

        private void Map(GetStudentSelectViewModel student, GetStudentOnTheLessonViewModel lesson)
        {
            student.FullName = lesson.StudentFullName;
            student.GroupName =  lesson.StudentGroupName;
            student.GroupId = lesson.StudentGroupId;
            student.Id = lesson.StudentId;
        }

        private async Task UpdateAsync()
        {
            var updateModel = new UpdateLessonViewModel()
            {
                Id = Id.Value,
                Duration = Model.Duration,
                LessonStart = Model.LessonStart,
                LessonType = Model.LessonType,
                TeacherId = Model.TeacherId,
                CourseId = Model.CourseId
            };

            var result = await HttpClient.PutAsync(@"api/Lesson/Update", JsonContent.Create(updateModel));

            if (result.IsSuccessStatusCode)
            {
                await JS.InvokeAsync<object>("alert", $"Successful updated! for id: '{updateModel.Id}'");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }
    }
}
