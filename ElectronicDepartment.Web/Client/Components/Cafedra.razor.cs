using ElectronicDepartment.Web.Shared.Cafedra;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Cafedra
    {
        private string Title { get; set; } = string.Empty;

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public int? Id { get; set; } = default;

        private BaseCafedraViewModel Model { get; set; } = new BaseCafedraViewModel();

        public async Task Success()
        {
            Console.WriteLine("Success");

            if (Id == default)
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
            Title = Id == default ? "Create " + nameof(Cafedra) : "Edit " + nameof(Cafedra) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (Id != default)
            {
                var result = await HttpClient.GetFromJsonAsync<BaseCafedraViewModel>($"/api/Cafedra/Get?id={Id}");

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"/api/Cafedra/Create", JsonContent.Create(Model));

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
            var updateModel = new UpdateCafedraViewModel()
            {
                Id = Id.Value,
                Description = Model.Description,
                Phone = Model.Phone,
                Name = Model.Name
            };

            var result = await HttpClient.PutAsync(@"/api/Cafedra/Update", JsonContent.Create(updateModel));

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
