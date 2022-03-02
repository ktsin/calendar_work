using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories.Interfaces
{
    public interface IProjectsRepository : IRepository<Project>
    {
        public Task<Project> AddUserToProject(int projectId, object uid);
    }
}