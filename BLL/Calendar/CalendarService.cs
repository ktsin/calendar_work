using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Calendar
{
    public class CalendarService
    {
        private readonly ICalendarEventsRepository _calendarEvents;
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _projects;
        private readonly IProjectTasksRepository _projectTasks;

        public CalendarService(ICalendarEventsRepository calendarEvents,
            IProjectTasksRepository projectTasks,
            IProjectsRepository projects,
            IMapper mapper)
        {
            _calendarEvents = calendarEvents;
            _projectTasks = projectTasks;
            _projects = projects;
            _mapper = mapper;
        }

        public async Task<Calendar> GetCalendarForUser(object userId, DateTime month)
        {
            Calendar calendar = new Calendar();
            calendar.Month = CalendarUtils.GetStartOfMonth((Month) month.Month, month.Year);
            int days = DateTime.DaysInMonth(month.Year, month.Month);
            calendar.CalendarDays = new List<CalendarDay>(days);
            for (int j = 0; j < days; j++)
            {
                calendar.CalendarDays.Add(
                    new CalendarDay()
                    {
                        Day = calendar.Month.AddDays(j)
                    });
            }

            // int i = 0;
            foreach (var day in calendar.CalendarDays)
            {
                // day.Day = calendar.Month.AddDays(i);
                var calendarEvents = await _calendarEvents
                    .GetBySelector(e => e.EventDate >= day.Day
                                        && e.EventDate < day.Day.AddDays(1)
                                        && e.OwnerId.Equals(userId));
                day.CalendarEvents = await Task
                    .Run(() => calendarEvents.Select(_mapper.Map<CalendarEventDTO>)
                        .ToList());
                var projectTasks = await _projectTasks.GetBySelector(e => e.TaskStart <= day.Day
                                                                          && e.TaskEnd >= day.Day.AddDays(1)
                                                                          && e.Participants.Contains(new User()
                                                                              {Id = userId as string}));
                day.ProjectTasks = await Task.Run(() => projectTasks.Select(_mapper.Map<ProjectTaskDTO>).ToList());
                // i++;
            }

            return calendar;
        }

        public async Task<CalendarDay> GetCalendarDay(object userId, DateTime date)
        {
            CalendarDay day = new();

            day.Day = date;
            var calendarEvents = await _calendarEvents
                .GetBySelector(e => e.EventDate >= day.Day
                                    && e.EventDate < day.Day.AddDays(1)
                                    && e.OwnerId.Equals(userId));
            day.CalendarEvents = await Task
                .Run(() => calendarEvents.Select(_mapper.Map<CalendarEventDTO>)
                    .ToList());
            var projectTasks = await _projectTasks.GetBySelector(e => e.TaskStart <= day.Day
                                                                      && e.TaskEnd >= day.Day.AddDays(1)
                                                                      && e.Participants.Contains(new User()
                                                                          {Id = userId as string}));
            day.ProjectTasks = await Task.Run(() => projectTasks.Select(_mapper.Map<ProjectTaskDTO>).ToList());

            return day;
        }

        public async Task<CalendarEventDTO> AddCalemdarEvent(CalendarEventDTO value)
        {
            return _mapper.Map<CalendarEventDTO>(
                await _calendarEvents.Create(_mapper.Map<CalendarEvent>(value))
            );
        }
    }
}