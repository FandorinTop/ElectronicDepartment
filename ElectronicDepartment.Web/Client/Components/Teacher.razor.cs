using ElectronicDepartment.Common.Enums;
using ElectronicDepartment.Web.Shared.Cafedra.Responce;
using ElectronicDepartment.Web.Shared.User.Teacher;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Teacher
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

        public AcademicAcredition[] AcademicAcreditions = new[]
        {
            AcademicAcredition.None,
            AcademicAcredition.Teacher,
            AcademicAcredition.Assistent,
            AcademicAcredition.SeniorLecture,
            AcademicAcredition.AssistanProfessor,
            AcademicAcredition.Professor
        };

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public string Id { get; set; }

        private BaseTeacherViewModel Model { get; set; } = new BaseTeacherViewModel();

        private GetCafedraSelectorViewModel[] CafedraSelectors { get; set; } = new GetCafedraSelectorViewModel[0];

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
            await GetCafedraSelector();

            Title = Id == null ? "Create " + nameof(Teacher) : "Edit " + nameof(Teacher) + $" with id: '{Id}'";
        }

        private async Task GetAsync()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                var result = await HttpClient.GetFromJsonAsync<BaseTeacherViewModel>($"api/Teacher/Get?id={Id}");

                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        private async Task CreateAsync()
        {
            var result = await HttpClient.PostAsync(@"api/Manager/CreateTeacher", JsonContent.Create(Model));

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
            var updateModel = new UpdateTeacherViewModel()
            {
                Id = Id,
                AcademicAcredition = Model.AcademicAcredition,
                BirthDay = Model.BirthDay,
                CafedraId = Model.CafedraId,
                Email = Model.Email,
                FirstName = Model.FirstName,
                MiddleName = Model.MiddleName,
                LastName = Model.LastName,
                Gender = Model.Gender,
                PhoneNumber = Model.PhoneNumber,
            };

            var result = await HttpClient.PutAsync(@"api/Manager/UpdateTeacher", JsonContent.Create(updateModel));

            if (result.IsSuccessStatusCode)
            {
                await JS.InvokeAsync<object>("alert", $"Successful updated! for id: '{updateModel.Id}'");
            }
            else
            {
                await JS.InvokeAsync<object>("alert", $"UpdateError {await result.Content.ReadAsStringAsync()}");
            }
        }

        private async Task GetCafedraSelector()
        {
            var result = await HttpClient.GetFromJsonAsync<IEnumerable<GetCafedraSelectorViewModel>>($"api/Cafedra/GetSelector");

            if (result.Any())
            {
                CafedraSelectors = result.ToArray();
            }

        }
    }
}
