﻿using ElectronicDepartment.Web.Shared.Course;
using ElectronicDepartment.Web.Shared.CourseTeacher.Responce;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Linq;
using MatBlazor;
using ElectronicDepartment.Web.Shared.Lesson.Responce;
using ElectronicDepartment.Web.Shared.Lesson;
using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.Web.Client.Components
{
    public partial class Course
    {
        private string Title { get; set; } = string.Empty;
        private int _index = -1;
        private bool _isDelete = false;
        private bool _isLessonEdit = false;
        private bool dialogIsOpen = false;
        private bool snackBar = false;

        private List<GetCourseTeacherSelectorViewModel> Teachers = new List<GetCourseTeacherSelectorViewModel>();
        private List<GetCourseLessonViewModel> Lessons = new List<GetCourseLessonViewModel>();
        private UpdateLessonViewModel Lesson { get; set; } = new UpdateLessonViewModel();
        GetCourseTeacherSelectorViewModel SelectedTeacher { get; set; }

        private LessonType[] LessonTypes = new LessonType[]
        {
            LessonType.None,
            LessonType.Practic,
            LessonType.Lab,
            LessonType.Lecture,
            LessonType.Exam,
        };

        [Inject]
        private HttpClient HttpClient { get; set; }

        [Inject]
        IJSRuntime JS { get; set; }

        [Parameter]
        public int? Id { get; set; } = default;

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
            await GetAsync();
            await LoadTeahers();

            Title = Id == null ? "Create " + nameof(Course) : "Edit " + nameof(Course) + $" with id: '{Id}'";
        }

        protected override Task OnAfterRenderAsync(bool firstRender)
        {
            matAutocomplete.Value = Teachers.FirstOrDefault(item => item.Id == Lesson.TeacherId) ?? Teachers.FirstOrDefault();
            return base.OnAfterRenderAsync(firstRender);
        }


        #region Lesson
        private void OpenDialog(bool isEdit)
        {
            _isLessonEdit = isEdit;

            if (!_isLessonEdit)
            {
                Lesson = new UpdateLessonViewModel();
            }

            dialogIsOpen = true;
        }

        private async Task CloseDialog()
        {
            dialogIsOpen = false;

            var teacher = Teachers.FirstOrDefault(item => item.Id == Lesson.TeacherId);
            var teacherName = string.Empty;

            if (teacher != null)
            {
                teacherName = $"{teacher.FirstName} {teacher.MiddleName} {teacher.LastName}";
            }

            if (!_isLessonEdit)
            {
                var lessonId = await CreateLessonRequestAsync(Lesson);

                var newViewModelItem = new GetCourseLessonViewModel()
                {
                    Id = lessonId,
                    Duration = Lesson.Duration,
                    LessonStart = Lesson.LessonStart,
                    LessonType = Lesson.LessonType,
                    TeacherFullName = teacherName,
                    TeacherId = Lesson.TeacherId,
                    TotalStudentOnLesson = 0
                };

                Lessons.Add(newViewModelItem);
            }
            else
            {
                await UpdateLessonRequestAsync(Lesson);

                var selected = Lessons.FirstOrDefault(item => item.Id == Lesson.Id);

                if (selected is not null)
                {
                    selected.Duration = Lesson.Duration;
                    selected.LessonStart = Lesson.LessonStart;
                    selected.LessonType = Lesson.LessonType;
                    selected.TeacherFullName = teacherName;
                    selected.TeacherId = Lesson.TeacherId;
                    selected.Duration = Lesson.Duration;
                }
            }
        }

        private async Task UpdateLessonRequestAsync(UpdateLessonViewModel lesson)
        {
            lesson.CourseId = Id.Value;

            var responce = await HttpClient.PutAsJsonAsync(@"api/lesson/update", lesson);

            if (responce.IsSuccessStatusCode)
            {
                return;
            }
            else
            {

            }

            throw new NotImplementedException();
        }

        private async Task<int> CreateLessonRequestAsync(UpdateLessonViewModel lesson)
        {
            lesson.CourseId = Id.Value;

            var Model = new CreateLessonViewModel()
            {
                CourseId = Id.Value,
                Duration = lesson.Duration,
                LessonStart = lesson.LessonStart,
                LessonType = lesson.LessonType,
                TeacherId = lesson.TeacherId
            };

            try
            {
                var responce = await HttpClient.PostAsJsonAsync(@"api/lesson/create", Model);

                var temp = HttpClient.BaseAddress + @"api/lesson/create";

                var context = await responce.Content.ReadAsStringAsync();

                if (responce.IsSuccessStatusCode)
                {
                    return Convert.ToInt32(await responce.Content.ReadAsStringAsync());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                
            }


            throw new NotImplementedException();
        }

        private async Task DeleteLessonAsync()
        {
            _isDelete = true;

            if (Lesson != null && Lesson.Id > 0)
            {
                var lesson = Lessons.FirstOrDefault(item => item.Id == Lesson.Id);

                if (lesson is null)
                {
                    return;
                }
                else
                {
                    await DeleteLessonRequest(lesson);
                    _isDelete = false;
                    StateHasChanged();
                }
            }
        }

        private async Task DeleteLessonRequest(GetCourseLessonViewModel lesson)
        {
            var responce = await HttpClient.DeleteAsync(@$"api/lesson/delete?id={lesson.Id}");

            throw new NotImplementedException();
        }

        #region MatTableSelection
        public async Task SelectionChangedEvent(object obj)
        {
            var lesson = obj as GetCourseLessonViewModel;

            if (lesson is null)
            {
                Lesson = new UpdateLessonViewModel();
            }
            else
            {
                Lesson = new UpdateLessonViewModel()
                {
                    Id = lesson.Id,
                    Duration = lesson.Duration,
                    LessonStart = lesson.LessonStart,
                    LessonType = lesson.LessonType,
                    TeacherId = lesson.TeacherId,
                };
            }

            if (_isDelete)
            {
                await DeleteLessonAsync();
            }
        }
        #endregion

        #endregion

        private async Task GetAsync()
        {
            if (Id != null)
            {
                var result = await HttpClient.GetFromJsonAsync<BaseCourseViewModel>($"/api/Course/Get?id={Id}");
                await LoadLessons();
                Console.WriteLine("getResult: " + result);

                Model = result ?? Model;
            }
        }

        #region Course
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
        #endregion

        private string CustomStringSelectorHandler(GetCourseTeacherSelectorViewModel item)
        {
            Lesson.TeacherId = item.Id;

            return item.LastName;
        }

        private async Task LoadTeahers()
        {
            var teachers = await HttpClient.GetFromJsonAsync<IEnumerable<GetCourseTeacherSelectorViewModel>>(@"api/CourseTeacher/GetSelector");

            Teachers = teachers.ToList();
        }

        private async Task LoadLessons()
        {
            var lessons = await HttpClient.GetFromJsonAsync<IEnumerable<GetCourseLessonViewModel>>(@$"api/Lesson/GetCourseLesson?courseId={Id.Value}");

            Lessons = lessons.ToList();
        }
    }
}
