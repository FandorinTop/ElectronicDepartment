using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Shared.Group.Responce;
using ElectronicDepartment.Web.Shared.User.Manager;
using ElectronicDepartment.Web.Shared.User.Manager.Responce;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Manager
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

        private BaseManagerViewModel Model { get; set; } = new BaseManagerViewModel();

        private GetGroupSelectorViewModel[] GroupSelectors { get; set; } = new GetGroupSelectorViewModel[0];

        private GetGroupSelectorViewModel SelectedGroup { get; set; }

        public async Task Success()
        {
            Console.WriteLine("Success");

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

            Title = Id == null ? "Create " + nameof(Manager) : "Edit " + nameof(Manager) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var result = await HttpClient.GetFromJsonAsync<BaseManagerViewModel>($"api/Manager/GetManager?id={Id}");

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"api/Manager/CreateManager", JsonContent.Create(Model));

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
            var updateModel = new UpdateManagerViewModel()
            {
                Id = Id,
                BirthDay = Model.BirthDay,
                Email = Model.Email,
                FirstName = Model.FirstName,
                MiddleName = Model.MiddleName,
                LastName = Model.LastName,
                Gender = Model.Gender,
                PhoneNumber = Model.PhoneNumber,
            };

            var result = await HttpClient.PutAsync(@"api/Manager/UpdateManager", JsonContent.Create(updateModel));

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
