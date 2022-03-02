using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DAL.Repositories.EFCore;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class Configure
    {
        public static IServiceCollection ConfigureDal(this IServiceCollection services, IConfiguration configuration)
        {
            string conStr = configuration.GetConnectionString("MainData");
            //string conStr = configuration.GetConnectionString("PMainData");
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opt.UseSqlite(conStr, e =>
                {
                    e.MigrationsAssembly("DumbCalendar");
                    e.MigrationsHistoryTable("MigrationsHistory");
                });
                // opt.UseNpgsql(conStr, e =>
                // {
                //     e.MigrationsAssembly("DumbCalendar");
                //     e.MigrationsHistoryTable("MigrationsHistory");
                // });
            });
            services.AddScoped<ICalendarEventsRepository, CalendarEventsRepository>();
            services.AddScoped<IGroupsRepository, GroupsRepository>();
            services.AddScoped<IMessagesRepository, MessagesRepository>();
            services.AddScoped<IProjectsRepository, ProjectsRepository>();
            services.AddScoped<IProjectTasksRepository, ProjectTasksRepository>();
            services.AddScoped<ITagsRepository, TagsRepository>();
            services.AddScoped<IUserRepository, UsersRepository>();
            
            return services;
        }
    }
}
