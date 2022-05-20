using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Shared.Group.Responce;
using ElectronicDepartment.Web.Shared.User.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Student
    {
        private string Title { get; set; } = string.Empty;

        [Inject]
        private HttpClient HttpClient { get; set; }

        public Gender[] Genders = new[]
        {
            Gender.None,
            Gender.Male,
            Gender.Female,
        };

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public string Id { get; set; }

        private BaseStudentViewModel Model { get; set; } = new BaseStudentViewModel();

        private GetGroupSelectorViewModel[] GroupSelectors { get; set; } = new GetGroupSelectorViewModel[0];

        private GetGroupSelectorViewModel SelectedGroup { get; set; }

        public async Task Success()
        {
            Console.WriteLine("Success");
            Model.GroupId = SelectedGroup?.Id ?? Model.GroupId;

            if (string.IsNullOrEmpty(Id))
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
            await GetGroupSelector();

            Title = Id == null ? "Create " + nameof(Student) : "Edit " + nameof(Student) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var result = await HttpClient.GetFromJsonAsync<BaseStudentViewModel>($"api/Student/Get?id={Id}");

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
                Id = id;
                await JS.InvokeAsync<object>("alert", $"Successful created! with id: '{id}'");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"CreationError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task UpdateAsync()
        {
            var updateModel = new UpdateStudentViewModel()
            {
                Id = Id,
                BirthDay = Model.BirthDay,
                GroupId = Model.GroupId,
                Email = Model.Email,
                FirstName = Model.FirstName,
                MiddleName = Model.MiddleName,
                LastName = Model.LastName,
                Gender = Model.Gender,
                PhoneNumber = Model.PhoneNumber,
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

        private async Task GetGroupSelector()
        {
            var result = await HttpClient.GetFromJsonAsync<IEnumerable<GetGroupSelectorViewModel>>($"api/Group/GetSelector");

            if (result.Any())
            {
                GroupSelectors = result.ToArray();
            }

        }

        private void OnNullGroup(object obj)
        {
            var res = obj as GetGroupSelectorViewModel;

            if (res is not null)
            {
                Model.GroupId = res.Id;
            }
        }
    }
}
