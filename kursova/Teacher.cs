using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class Teacher : ApplizationUser
    {
        public AcademicAcredition AcademicAcredition { get; set; }
    }
}