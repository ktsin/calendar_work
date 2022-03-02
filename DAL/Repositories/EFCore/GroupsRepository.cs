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
    public class GroupsRepository : IGroupsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<GroupsRepository> _logger;

        public GroupsRepository(ILogger<GroupsRepository> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Group> Create(Group value)
        {
            var res = await _context.Groups.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            return res?.Entity;
        }

        public async Task<Group> Update(Group value)
        {
            var res = _context.Groups.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var res = _context.Groups.Remove(GetById(key).Result);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<Group>> ReadAll()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<ICollection<Group>> ReadAllInclude()
        {
            return await _context.Groups.Include(e => e.GroupParticipants).ToListAsync();
        }

        public async Task<ICollection<Group>> GetBySelector(Func<Group, bool> selector)
        {
            return await Task.Run(() => _context
                .Groups
                .Include(e => e.GroupParticipants)
                .Where(selector)
                .ToList());
        }

        public async Task<Group> GetById(object id)
        {
            return await _context.Groups.Include(e => e.GroupParticipants).FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
    }
}