using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services
{
    public interface IProjectService
    {
        Task<ICollection<ProjectDTO>> GetUsersProjects(object uid);
        Task<ICollection<ProjectDTO>> UserOwnedProjects(object uid);
        Task<ProjectDTO> GetProjectById(int projectId);
        Task<ProjectTaskDTO> GetTaskById(int taskId);
        Task<ProjectDTO> AddProject(ProjectDTO project);
        Task<ProjectDTO> UpdateProject(ProjectDTO project);
        Task<bool> RemoveProject(int projectId);
        Task<ProjectTaskDTO> AddProjectTask(ProjectTaskDTO projectTask);
        Task<ProjectTaskDTO> UpdateProjectTask(ProjectTaskDTO project);
        Task<bool> RemoveProjectTask(int taskId);
        Task<ProjectTaskDTO> AddUserToProjectTask(int taskId, object userId);
        Task<ProjectDTO> AddUserToProject(int projectId, object userId);
    }
}