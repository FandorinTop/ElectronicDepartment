using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Client.Services;
using ElectronicDepartment.Web.Shared.Group.Responce;
using ElectronicDepartment.Web.Shared.User.Student;
using ElectronicDepartment.Web.Shared.User.Student.Responce;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Student
    {
        private string Title { get; set; } = string.Empty;

        [Inject]
        IHttpService HttpClient { get; set; }

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
                var responce = await HttpClient.GetAsync($"api/Manager/GetStudent?id={Id}");
                var result = await responce.Content.ReadFromJsonAsync<BaseStudentViewModel>();

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"api/Manager/CreateStudent", Model);

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

            var result = await HttpClient.PutAsync(@"api/Manager/UpdateStudent", updateModel);

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
            var responce = await HttpClient.GetAsync($"api/Group/GetSelector");
            var result = await responce.Content.ReadFromJsonAsync<IEnumerable<GetGroupSelectorViewModel>>();

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
