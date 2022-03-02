using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL.Repositories.EFCore
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<ProjectsRepository> _logger;

        public ProjectsRepository(ILogger<ProjectsRepository> logger,
            DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Project> Create(Project value)
        {
            var res = await _context.Projects.AddAsync(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            return res?.Entity;
        }

        public async Task<Project> Update(Project value)
        {
            var res = _context.Projects.Update(value);
            var saveRes = await _context.SaveChangesAsync();
            res.State = EntityState.Detached;
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return res?.Entity;
        }

        public async Task<bool> Delete(object key)
        {
            var res = _context.Projects.Remove(GetById(key).Result);
            var saveRes = await _context.SaveChangesAsync();
            _logger.LogDebug(new EventId(1212), res?.DebugView?.LongView);
            return true;
        }

        public async Task<ICollection<Project>> ReadAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ICollection<Project>> ReadAllInclude()
        {
            return await _context
                .Projects.AsNoTracking()
                .Include(e => e.Participants)
                .Include(e => e.Tasks)
                .ToListAsync();
        }

        public async Task<ICollection<Project>> GetBySelector(Func<Project, bool> selector)
        {
            return await Task.Run(() => _context.Projects.AsNoTracking()
                .Include(e => e.Participants)
                .Include(e => e.Tasks)
                .Where(selector)
                .ToList());
        }

        public async Task<Project> GetById(object id)
        {
            return await _context.Projects.AsNoTracking()
                .Include(e => e.Participants)
                .Include(e => e.Tasks)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<Project> AddUserToProject(int projectId, object uid)
        {
            SqliteParameter pId = new SqliteParameter("pId", projectId);
            SqliteParameter uId = new SqliteParameter("uId", uid);
            await _context.Database
                .ExecuteSqlRawAsync(@"INSERT INTO UserProject (InProjectsId, ParticipantsId) VALUES (@pId, @uId)", pId,
                    uId);
            await _context.SaveChangesAsync();
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(e => e.Id == projectId);
        }
    }
}