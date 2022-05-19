using ElectronicDepartment.Common.Enums;

namespace ElectronicDepartment.DomainEntities
{
    public class Teacher : ApplizationUser
    {
        public AcademicAcredition AcademicAcredition { get; set; }

        public int CafedraId { get; set; }

        public virtual Cafedra Cafedra { get; set; }
    }
}