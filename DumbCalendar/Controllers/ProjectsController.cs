using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services;
using DumbCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DumbCalendar.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly IProjectService _projectService;
        private readonly IUserDataService _userDataService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(UserManager<IdentityUser> userManager, IUserDataService userDataService,
            IProjectService projectService, IGroupService groupService)
        {
            _userManager = userManager;
            _userDataService = userDataService;
            _projectService = projectService;
            _groupService = groupService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ProjectsIndexViewModel();
            model.UserOwn = await _projectService.GetUsersProjects(_userManager.GetUserId(User));
            model.Participating = await _projectService.UserOwnedProjects(_userManager.GetUserId(User));
            return View(model);
        }

        public async Task<IActionResult> ProjectInfo(int id)
        {
            var project = await _projectService.GetProjectById(id);
            return PartialView("_projectDetails", project);
        }

        [HttpGet]
        public async Task<IActionResult> AddParticipantToProject(int id)
        {
            var availableUsers = await _groupService.GetUserGroups(_userManager.GetUserId(User));
            var select = availableUsers.Select(e => e.GroupParticipants);
            var list = new List<UserDTO>();
            foreach (var col in select)
            {
                list.AddRange(col);
            }

            var options = list
                .Distinct()
                .Select(e => new {Id = e.Id, FullName = e.FullName})
                .ToList();
            ViewBag.SelectOpts = new MultiSelectList(options, "Id", "FullName");
            return PartialView("_addToProject", new AddParticipantViewModel()
            {
                AvailableUsers = list,
                SelectedUser = ViewBag.SelectOpts,
                TargetId = id
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddParticipantToProject(string[] id)
        {
            int projectId = Int32.Parse(Request.Form["TargetId"]);
            id = id.Distinct().ToArray();
            foreach (var uid in id)
            {
                await _projectService.AddUserToProject(projectId, uid);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddProjectTask(int id)
        {
            var projects = await _projectService.UserOwnedProjects(_userManager.GetUserId(User));
            ViewBag.ProjectId = new MultiSelectList(
                projects.Select(e => new {Id = e.Id, Name = e.Name}),
                "Id",
                "Name"
            );
            return PartialView("_addProjectTask", new ProjectTaskDTO() {ProjectId = id});
        }

        [HttpPost]
        public async Task<IActionResult> AddProjectTask(ProjectTaskDTO task)
        {
            await _projectService.AddProjectTask(task);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddProject()
        {
            return PartialView("_addProject", new ProjectDTO() {ProjectOwner = _userManager.GetUserId(User)});
        }

        public async Task<IActionResult> AddProject(ProjectDTO project)
        {
            await _projectService.AddProject(project);
            return RedirectToAction("Index");
        }
    }
}