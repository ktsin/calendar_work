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

namespace DumbCalendar.Controllers
{
    [Authorize]
    public class ProjectTaskController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectTaskController(IProjectService projectService, UserManager<IdentityUser> userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetUsersProjects(_userManager.GetUserId(User));
            
            return View(new ProjectTasksViewModel()
            {
                Participating = new List<ProjectTaskDTO>(),
                UserOwn = new List<ProjectTaskDTO>()
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await _projectService.GetTaskById(id);
            return View(task);
        }

        public async Task<IActionResult> AddUserToTask(int id)
        {
            var task = await _projectService.GetTaskById(id);
            return PartialView("_addToTask", new AddParticipantViewModel(){TargetId = task.Id});
        }
        
        [HttpPost]
        public async Task<IActionResult> AddUserToTask(string[] id)
        {
            foreach (var i in id.Distinct())
            {
                await _projectService.AddUserToProjectTask(Int32.Parse(Request.Form["TargetId"]), i);
            }

            return View("Message", new MessageViewModel()
            {
                Caption = "Users are added!",
                Message = "",
                ReturnUrl = "/Projects"
            });
        }
        
        
    }
}