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
    public class TagsRepository : ITagsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<TagsRepository> _logger;

        public TagsRepository(DataContext context, ILogger<TagsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Tag> Create(Tag value)
        {
            var res = await _context.Tags.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            return res?.Entity;
        }

        public async Task<Tag> Update(Tag value)
        {
            var res = _context.Tags.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var res = _context.Tags.Remove(GetById(key).Result);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<Tag>> ReadAll()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<ICollection<Tag>> ReadAllInclude()
        {
            return await _context.Tags
                .Include(e => e.CalendarEvents)
                .Include(e => e.ProjectTasks)
                .ToListAsync();
        }

        public async Task<ICollection<Tag>> GetBySelector(Func<Tag, bool> selector)
        {
            return await Task.Run(() => _context.Tags
                .Include(e => e.CalendarEvents)
                .Include(e => e.ProjectTasks)
                .Where(selector).ToList());
        }

        public async Task<Tag> GetById(object id)
        {
            return await _context.Tags
                .Include(e => e.CalendarEvents)
                .Include(e => e.ProjectTasks)
                .FirstOrDefaultAsync(e => e.OwnerId.Equals(id));
        }
    }
}