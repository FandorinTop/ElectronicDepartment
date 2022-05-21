namespace ElectronicDepartment.Web.Shared.Mark.Responce
{
    public class GetStudentSelectViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public List<int> LessonIds { get; set; } = new List<int>();
    }

}
