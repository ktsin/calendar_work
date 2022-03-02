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
    public class MessagesRepository : IMessagesRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<MessagesRepository> _logger;

        public MessagesRepository(ILogger<MessagesRepository> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Message> Create(Message value)
        {
            var res = await _context.Messages.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            return res?.Entity;
        }

        public async Task<Message> Update(Message value)
        {
            var res = _context.Messages.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var res = _context.Messages.Remove(GetById(key).Result);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<Message>> ReadAll()
        {
            return await _context.Messages.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Message>> ReadAllInclude()
        {
            return await _context.Messages.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<Message>> GetBySelector(Func<Message, bool> selector)
        {
            return await Task.Run(() => _context.Messages.AsNoTracking().Where(selector).ToList());
        }

        public async Task<Message> GetById(object id)
        {
            return await _context.Messages.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
    }
}