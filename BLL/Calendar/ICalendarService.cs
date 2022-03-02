using System;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Calendar
{
    public interface ICalendarService
    {
        Task<Calendar> GetCalendarForUser(object userId, DateTime month);
        Task<CalendarDay> GetCalendarDay(object userId, DateTime date);
        Task<CalendarEventDTO> AddCalemdarEvent(CalendarEventDTO value);
    }
}