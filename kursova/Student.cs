namespace ElectronicDepartment.DomainEntities
{
    public class Student : ApplizationUser
    {
        public int GroupId { get; set; }

        public virtual Group Group { get; set; } = default!;
    }
}