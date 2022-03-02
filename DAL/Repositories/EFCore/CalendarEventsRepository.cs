using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.EFCore
{
    public class CalendarEventsRepository : ICalendarEventsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CalendarEventsRepository> _logger;

        public CalendarEventsRepository(DataContext context,
            ILogger<CalendarEventsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<CalendarEvent> Create(CalendarEvent value)
        {
            var res = await _context.CalendarEvents.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            return res?.Entity;
        }

        public async Task<CalendarEvent> Update(CalendarEvent value)
        {
            var res = _context.CalendarEvents.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var res = _context.CalendarEvents.Remove(GetById(key).Result);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<CalendarEvent>> ReadAll()
        {
            return await _context.CalendarEvents.ToListAsync();
        }

        public async Task<ICollection<CalendarEvent>> ReadAllInclude()
        {
            return await _context.CalendarEvents.Include(e => e.Tags).ToListAsync();
        }

        public async Task<ICollection<CalendarEvent>> GetBySelector(Func<CalendarEvent, bool> selector)
        {
            return await Task.Run(()=>_context.CalendarEvents.Where(selector).ToList());
        }

        public async Task<CalendarEvent> GetById(object id)
        {
            return await _context.CalendarEvents.FirstOrDefaultAsync(e=>e.OwnerId.Equals(id));
        }
    }
}