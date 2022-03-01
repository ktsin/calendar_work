using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace BLL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ILogger<ProjectService> _logger;
        private readonly IMapper _mapper;
        private readonly IProjectsRepository _projects;
        private readonly IProjectTasksRepository _projectTasks;
        private readonly IUserRepository _userRepository;

        public ProjectService(IProjectTasksRepository projectTasks, IProjectsRepository projects, IMapper mapper,
            ILogger<ProjectService> logger, IUserRepository userRepository)
        {
            _projectTasks = projectTasks;
            _projects = projects;
            _mapper = mapper;
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<ICollection<ProjectDTO>> GetUsersProjects(object uid)
        {
            var projects = await _projects.GetBySelector(e => e.Participants.Any(f => f.Id.Equals(uid)));
            return projects.Select(_mapper.Map<ProjectDTO>).ToList();
        }

        public async Task<ICollection<ProjectDTO>> UserOwnedProjects(object uid)
        {
            var projects = await _projects.GetBySelector(e => e.ProjectOwner.Equals(uid));
            return projects.Select(_mapper.Map<ProjectDTO>).ToList();
        }

        public async Task<ProjectDTO> GetProjectById(int projectId)
        {
            var project = await _projects.GetById(projectId);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectTaskDTO> GetTaskById(int taskId)
        {
            var task = await _projectTasks.GetById(taskId);
            return _mapper.Map<ProjectTaskDTO>(task);
        }

        public async Task<ProjectDTO> AddProject(ProjectDTO project)
        {
            try
            {
                var source = await _projects.Create(_mapper.Map<Project>(project));
                return _mapper.Map<ProjectDTO>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in AddProject(ProjectDTO project)");
            }

            return project;
        }

        public async Task<ProjectDTO> UpdateProject(ProjectDTO project)
        {
            try
            {
                var source = await _projects.Update(_mapper.Map<Project>(project));
                return _mapper.Map<ProjectDTO>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateProject(ProjectDTO project)");
            }

            return project;
        }

        public async Task<bool> RemoveProject(int projectId)
        {
            bool res = true;
            try
            {
                var remRes = await _projects.Delete(projectId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateProject(ProjectDTO project)");
                res = false;
            }

            return res;
        }

        public async Task<ProjectTaskDTO> AddProjectTask(ProjectTaskDTO projectTask)
        {
            try
            {
                var source = await _projectTasks.Create(_mapper.Map<ProjectTask>(projectTask));
                return _mapper.Map<ProjectTaskDTO>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in AddProjectTask(ProjectTaskDTO projectTask)");
            }

            return projectTask;
        }

        public async Task<ProjectTaskDTO> UpdateProjectTask(ProjectTaskDTO projectTask)
        {
            try
            {
                var source = await _projectTasks.Update(_mapper.Map<ProjectTask>(projectTask));
                return _mapper.Map<ProjectTaskDTO>(source);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in AddProjectTask(ProjectTaskDTO projectTask)");
            }

            return projectTask;
        }

        public async Task<bool> RemoveProjectTask(int taskId)
        {
            bool res = true;
            try
            {
                var remRes = await _projectTasks.Delete(taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in UpdateProjectTask(ProjectTaskDTO project)");
                res = false;
            }
            return res;
        }

        public async Task<ProjectTaskDTO> AddUserToProjectTask(int taskId, object userId)
        {
            var task = await _projectTasks.AddUserToTask(taskId, userId);
            return _mapper.Map<ProjectTaskDTO>(task);
        }

        public async Task<ProjectDTO> AddUserToProject(int projectId, object userId)
        {
            var nproject = await _projects.AddUserToProject(projectId, userId);
            return _mapper.Map<ProjectDTO>(nproject);
        }
    }
}