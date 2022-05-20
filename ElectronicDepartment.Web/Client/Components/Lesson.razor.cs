using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Shared.Course.Responce;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;
using ElectronicDepartment.Web.Shared.Lesson;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Lesson
    {
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

        private BaseLessonViewModel Model { get; set; } = new BaseLessonViewModel();

        private GetCourseTeacherSelectorViewModel[] TeacherSelectors { get; set; } = new GetCourseTeacherSelectorViewModel[0];

        private GetCourseTeacherSelectorViewModel SelectedTeacher { get; set; }

        private GetCourseSelectorViewModel[] CourseSelectors { get; set; } = new GetCourseSelectorViewModel[0];

        private GetCourseSelectorViewModel SelectedCourse { get; set; }

        public async Task Success()
        {
            Console.WriteLine("Success");
            Model.CourseTeacherId = SelectedTeacher?.Id ?? Model.CourseTeacherId;
            Model.CourseId = SelectedTeacher?.Id ?? Model.CourseId;

            if (Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            await GetAsync();
            await GetCourseTeacherSelector();

            Title = Id == null ? "Create " + nameof(Student) : "Edit " + nameof(Student) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (Id == null)
            {
                var result = await HttpClient.GetFromJsonAsync<BaseLessonViewModel>($"api/Lesson/Get?id={Id}");

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"api/Manager/CreateStudent", JsonContent.Create(Model));

            if (result.IsSuccessStatusCode)
            {
                var id = await result.Content.ReadAsStringAsync();
                Id = Convert.ToInt32(id);
                await JS.InvokeAsync<object>("alert", $"Successful created! with id: '{id}'");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"CreationError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task UpdateAsync()
        {
            var updateModel = new UpdateLessonViewModel()
            {
                Id = Id.Value,
                CourseId = Model.CourseId,
                Duration = Model.Duration,
                LessonStart = Model.LessonStart,
                LessonType = Model.LessonType,
                CourseTeacherId = Model.CourseTeacherId
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

        private async Task GetCourseTeacherSelector()
        {
            var result = await HttpClient.GetFromJsonAsync<IEnumerable<GetCourseTeacherSelectorViewModel>>($"api/CourseTeacher/GetSelector");

            if (result.Any())
            {
                TeacherSelectors = result.ToArray();
            }
        }

        private async Task GetCourseSelector()
        {
            var result = await HttpClient.GetFromJsonAsync<IEnumerable<GetCourseSelectorViewModel>>($"api/CourseTeacher/GetSelector");

            if (result.Any())
            {
                CourseSelectors = result.ToArray();
            }
        }
    }
}
