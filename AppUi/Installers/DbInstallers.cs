using AppDataAccess.Data;
using AppDataAccess.Helpers;
using AppDataAccess.Repositories;
using AppUi.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppUi.Installers
{
    public class DbInstallers : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<DataContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly("AppUi")));

            services.AddDefaultIdentity<IdentityUser>(options => 
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddScoped<IChildServices, ChildService>();
            services.AddScoped<IYearsSemServices, YearSemServices>();
            services.AddScoped<IUsersServices, UserServices>();
            services.AddScoped<ISubjectServices, SubjectServices>();
            services.AddScoped<IClassServices, ClassServices>();
            services.AddScoped<IFeaturesServices, FeatureServices>();
            services.AddScoped<IVolunteerServices, VolunteerServices>();
            services.AddScoped<ITimelineServices, TimelinesServices>();
            services.AddScoped<IGalleryServices, GalleryServices>();
            services.AddScoped<IBlogServices, BlogServices>();
            services.AddScoped<IStoriesServies, StoriesServices>();
            services.AddScoped<IEventsServices, EventsServices>();
            services.AddScoped<ILettersServices, LettersServices>();
            services.AddScoped<IVisitationServices, VisitationServices>();
            services.AddScoped<ITimeLinesServices, TimeLineServices>();
            services.AddScoped<INotificationServices, NotificationServies>();
            services.AddScoped<IPrayerSupportServices, PrayerSupportServices>();
        }
    }
}
