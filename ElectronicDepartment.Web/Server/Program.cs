using ElectronicDepartment.BusinessLogic;
using ElectronicDepartment.DataAccess;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.Extensions.DependencyInjection;
using ElectronicDepartment.DomainEntities;
using Microsoft.AspNetCore.Identity;
using static ElectronicDepartment.BusinessLogic.Helpers.FirstInit;
using static ElectronicDepartment.Common.Constants;
using ElectronicDepartment.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static Company.WebApplication1.StartupConfiguration;

namespace Company.WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

            builder.Services.AddIdentityCore<ApplicationUser>(opts =>
            {
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = AuthOptions.ISSUER,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = AuthOptions.AUDIENCE,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };
                });

            builder.Services.AddInjection();
            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = ("swagger/docs");
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureExceptionHandler(app.Environment, app.Logger);

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapDefaultControllerRoute();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            InitDb(app);

            

            app.Run();
        }
    }

    public static class StartupConfiguration
    {
        public static void AddInjection(this IServiceCollection services)
        {
            services.AddScoped<IUserManagerService, ManagerService>();
            services.AddScoped<ICafedraService, CafedraService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<ICourseTeacherService, CourseTeacherService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IStudentOnLessonService, StudentOnLessonService>();
            services.AddScoped<IGroupService, GroupService>();
        }

        public static void InitDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                InitRoles(roleManager, GetRoles()).GetAwaiter().GetResult();
                InitStartData(userManager, context).GetAwaiter().GetResult();
            }
            using (var scope = app.Services.CreateScope())
            {
                //Resolve ASP .NET Core Identity with DI help
                var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            }
        }

    }
}