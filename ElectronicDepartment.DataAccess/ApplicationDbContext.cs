using ElectronicDepartment.DomainEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ElectronicDepartment.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplizationUser>
    {
        public virtual DbSet<Mark> Marks { get; set; } = default!;

        public virtual DbSet<Group> Groups { get; set; } = default!;

        public virtual DbSet<Student> Students { get; set; } = default!;

        public virtual DbSet<Teacher> Teachers { get; set; } = default!;
        
        public virtual DbSet<Lesson> Lessons { get; set; } = default!;

        public virtual DbSet<CourseTeacher> CourseTeachers { get; set; } = default!; 
        public virtual DbSet<Course> Courses { get; set; } = default!; 
    }
}