using ElectronicDepartment.Web.Shared.Login;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ElectronicDepartment.Web.Client.Services
{

    public interface IAuthenticationService
    {
        LoginResult User { get; }
        Task Initialize();
        Task Login(string username, string password);
        Task Logout();
    }

    public class AuthenticationService : IAuthenticationService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;

        public LoginResult User { get; private set; }

        public AuthenticationService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }


        public async Task Initialize()
        {
            User = await _localStorageService.GetItem<LoginResult>("user");

            var t = "";
        }

        public async Task Login(string username, string password)
        {
            var loginModel = new LoginModel()
            {
                Email = username,
                Password = password
            };

            var responce = await _httpService.PostAsync("api/manager/login", loginModel);
            User =  await responce.Content.ReadFromJsonAsync<LoginResult>();
            await _localStorageService.SetItem("user", User);
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem("user");
            _navigationManager.NavigateTo("login");
        }
    }
}
