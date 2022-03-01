using System;
using BLL.Calendar;
using BLL.Services;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class Configure
    {
        public static IServiceCollection ConfigureBLL(this IServiceCollection services, IConfiguration config)
        {
            services.ConfigureDal(config);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<IUserDataService, UserDataService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IProjectService, ProjectService>();

            return services;
        }
    }
}