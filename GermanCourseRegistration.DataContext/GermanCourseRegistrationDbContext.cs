using GermanCourseRegistration.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GermanCourseRegistration.DataContext;

public class GermanCourseRegistrationDbContext : DbContext
{
    public GermanCourseRegistrationDbContext(
        DbContextOptions<GermanCourseRegistrationDbContext> options) : base(options)
    { }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseMaterial> CourseMaterials { get; set; }
    public DbSet<CourseMaterialOrder> CourseMaterialOrders { get; set; }
    public DbSet<CourseMaterialOrderItem> CourseMaterialOrderItems { get; set; }
    public DbSet<CourseOffer> CourseOffers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Timetable> Timetables { get; set; }
}
