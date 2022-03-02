using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.EFCore
{
    public class UsersRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(ILogger<UsersRepository> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<User> Create(User value)
        {
            var res = await _context.Users.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            return res?.Entity;
        }

        public async Task<User> Update(User value)
        {
            var res = _context.Users.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var target = await GetById(key);
            var res = _context.Users.Remove(target);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<User>> ReadAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<ICollection<User>> ReadAllInclude()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ICollection<User>> GetBySelector(Func<User, bool> selector)
        {
            return await Task.Run(() => _context.Users.AsNoTracking().Where(selector).ToList());
        }

        public async Task<User> GetById(object id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
    }
}