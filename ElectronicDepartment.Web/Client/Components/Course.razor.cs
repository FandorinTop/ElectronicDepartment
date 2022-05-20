using ElectronicDepartment.Web.Shared.Course;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Linq;
using MatBlazor;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Course
    {
        private List<GetCourseTeacherSelectorViewModel> AddTeacherList = new List<GetCourseTeacherSelectorViewModel>();
        private List<GetCourseTeacherSelectorViewModel> RemoveTeacherList = new List<GetCourseTeacherSelectorViewModel>();
        private MatAutocompleteList<GetCourseTeacherSelectorViewModel> MatAutocompleteList = null;
        private GetCourseTeacherSelectorViewModel Test = new GetCourseTeacherSelectorViewModel();

        private string Title { get; set; } = string.Empty;

        private string MatSelectorLabel { get
            {
                return FreeTeacherContainer.Any() ? "Select Teacher" : "No more elements";
            }
        }

        private string TableHeader
        {
            get
            {
                return CourseTeacherContainer.Any() ? "Double click to remove teacher from course" : "No teacher on this course";
            }
        }

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public int? Id { get; set; } = default;

        public List<GetCourseTeacherSelectorViewModel> Teachers = new List<GetCourseTeacherSelectorViewModel>();

        public List<GetCourseTeacherSelectorViewModel> CourseTeacherContainer = new List<GetCourseTeacherSelectorViewModel>();
        public List<GetCourseTeacherSelectorViewModel> FreeTeacherContainer = new List<GetCourseTeacherSelectorViewModel>();

        private BaseCourseViewModel Model { get; set; } = new BaseCourseViewModel();

        public async Task Success()
        {
            Console.WriteLine("Success");

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
            await LoadTeahers();
            await GetAsync();
            Title = Id == null ? "Create " + nameof(Course) : "Edit " + nameof(Course) + $" with id: '{Id}'";
            
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            MatAutocompleteList.ClearText(new EventArgs());
            return base.OnAfterRenderAsync(firstRender);
        }

        private async Task GetAsync()
        {
            if (Id != default)
            {
                var result = await HttpClient.GetFromJsonAsync<BaseCourseViewModel>($"/api/Course/Get?id={Id}");

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"/api/Course/Create", JsonContent.Create(Model));

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
            var updateModel = new UpdateCourseViewModel()
            {
                Id = Id.Value,
                Description = Model.Description,
                Name = Model.Name
            };

            var result = await HttpClient.PutAsync(@"/api/Course/Update", JsonContent.Create(updateModel));

            if (result.IsSuccessStatusCode)
            {
                await JS.InvokeAsync<object>("alert", $"Successful updated! for id: '{updateModel.Id}'");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task LoadTeahers()
        {
            var teachers = await HttpClient.GetFromJsonAsync<IEnumerable<GetCourseTeacherSelectorViewModel>>($"/api/CourseTeacher/GetSelector");

            if (Id != null)
            {
                FreeTeacherContainer = teachers
                    .Where(item => item.CourseIds.Any(item => item != Id.Value))
                    .ToList();
                CourseTeacherContainer = teachers
                    .Where(item => item.CourseIds.Any(item => item == Id.Value))
                    .ToList();
            }
            else
            {
                FreeTeacherContainer = teachers.ToList();
            }
        }

        //TODO RISING ON EVENT
        private void AddTeacherToCourse(GetCourseTeacherSelectorViewModel item)
        {
            if(item is null)
            {
                return;
            }

            FreeTeacherContainer.Remove(item);
            CourseTeacherContainer.Add(item);
            AddTeacherList.Add(item);
        }

        private void RemoveTeacherFromCourse(object obj)
        {
            var item = obj as GetCourseTeacherSelectorViewModel;

            if (item is null)
            {
                return;
            }

            CourseTeacherContainer.Remove(item);
            FreeTeacherContainer.Add(item);
            RemoveTeacherList.Add(item);
        }
    }
}
