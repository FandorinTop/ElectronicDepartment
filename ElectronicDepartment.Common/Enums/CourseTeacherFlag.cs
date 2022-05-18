namespace ElectronicDepartment.Common.Enums
{
    [Flags]
    public enum CourseTeacherFlag
    {
        None = 0,
        Lecture = 1 << 1,
        Practics = 1 << 2,
        Labs = 1 << 3
    }
}