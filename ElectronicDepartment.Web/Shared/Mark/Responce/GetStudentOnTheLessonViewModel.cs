namespace ElectronicDepartment.Web.Shared.Mark.Responce
{
    public class GetStudentOnTheLessonViewModel
    {
        public int Id { get; set; }
        public int StudentGroupId { get; set; }
        public string StudentId { get; set; }
        public string StudentGroupName { get; set; }
        public int? Mark { get; set; }
        public string StudentFullName { get; set; }
    }

}
