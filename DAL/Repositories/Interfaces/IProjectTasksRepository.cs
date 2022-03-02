using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IProjectTasksRepository : IRepository<ProjectTask>
    {
        public Task<ProjectTask> AddUserToTask(int taskId, object uid);

        public Task<ICollection<ProjectTask>> GetTasksByUserParticipating(object uid);
    }
}