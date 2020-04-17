using AppModels.DTO;
using Microsoft.EntityFrameworkCore;

namespace AppDataAccess.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        public DbSet<AcademicYear> AcademicYears { get; set; }
        public DbSet<Anouncement> Anouncements { get; set; }
        public DbSet<AnouncementSub> AnouncementSubs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<ChildClass> ChildClasses { get; set; }
        public DbSet<ChildFeature> ChildFeatures { get; set; }
        public DbSet<ChildSponser> ChildSponsers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassSubject> ClassSubjects { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<PrayerVolunteer> PrayerVolunteers { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<SemesterRegistration> SemesterRegistrations { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TimeLine> TimeLines { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogSub> BlogSubs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
