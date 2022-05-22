using ElectronicDepartment.Web.Client.Services;
using ElectronicDepartment.Web.Shared.Group;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Group
    {
        private string Title { get; set; } = string.Empty;

        [Inject]
        IHttpService HttpClient { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public int? Id { get; set; } = default;

        private BaseGroupViewModel Model { get; set; } = new BaseGroupViewModel();

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
            await GetAsync();
            Title = Id == null ? "Create " + nameof(Group) : "Edit " + nameof(Group) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (Id != default)
            {
                var responce = await HttpClient.GetAsync($"api/Group/Get?id={Id}");
                var result = await responce.Content.ReadFromJsonAsync<BaseGroupViewModel>();

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"api/Group/Create", JsonContent.Create(Model));

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
            var updateModel = new UpdateGroupViewModel()
            {
                Id = Id.Value,
                Name = Model.Name
            };

            var result = await HttpClient.PutAsync(@"api/Group/Update", JsonContent.Create(updateModel));

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
