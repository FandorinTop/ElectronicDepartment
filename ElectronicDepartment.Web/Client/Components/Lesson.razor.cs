using ElectronicDepartment.Common.Enums;
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
        private List<GetStudentSelectViewModel> FreeStudentSelectors { get; set; } = new List<GetStudentSelectViewModel>();
        private List<GetStudentOnTheLessonViewModel> StudentInLessons { get; set; } = new List<GetStudentOnTheLessonViewModel>();

        private UpdateLessonViewModel Model { get; set; } = new UpdateLessonViewModel();

        private UpdateMarkViewModel StudentOnLesson { get; set; } = new UpdateMarkViewModel();

        private string Title { get; set; } = string.Empty;

        [Inject]
        private HttpClient HttpClient { get; set; }

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
            if (Id == null)
            {
                var result = await HttpClient.GetFromJsonAsync<UpdateLessonViewModel>($"api/Lesson/Get?id={Id}");
                await GetStudentAsync();
                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task GetStudentAsync()
        {
            var studentValue = await HttpClient.GetFromJsonAsync<List<GetStudentSelectViewModel>>($"api/Mark/GetStudentLesson");

            if(studentValue != null)
            {
                FreeStudentSelectors.AddRange(studentValue.Where(item => item.LessonIds.Contains(Id.Value)));
            }
        }

        private async Task GetStudentOnLesson()
        {
            var studentValue = await HttpClient.GetFromJsonAsync<List<GetStudentOnTheLessonViewModel>>($"api/mark/GetStudentOnLesson?id={Id}");

            StudentInLessons = studentValue ?? new List<GetStudentOnTheLessonViewModel>();
        }

        #region Requests
        private async void AddStudentOnLessonAsync(UpdateMarkViewModel addViewModel)
        {
            var result = await HttpClient.PostAsJsonAsync(@"api/lesson/create", addViewModel);

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

                    await JS.InvokeAsync<object>("alert", $"Successful added student on lesson");
                }
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async void RemoveFromLessonAsync(GetStudentOnTheLessonViewModel removeViewModel)
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
                    FreeStudentSelectors.Remove(freeStudent);

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

            var result = await HttpClient.PutAsync(@"api/Manager/UpdateStudent", JsonContent.Create(updateModel));

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
