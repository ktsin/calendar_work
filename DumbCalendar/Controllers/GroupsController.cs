using System.Threading.Tasks;
using BLL.DTO;
using BLL.Services;
using DumbCalendar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DumbCalendar.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly IEmailSender _emailSender;
        private readonly IGroupService _groupService;
        private readonly UserManager<IdentityUser> _userManager;

        public GroupsController(IGroupService groupService,
            UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _groupService = groupService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var groups = await _groupService.GetUserGroups(_userManager.GetUserId(User));
            return View(groups);
        }

        [HttpGet]
        public async Task<IActionResult> GroupInvite(int id)
        {
            var group = await _groupService.GetById(id);
            var userId = _userManager.GetUserId(User);
            //var res = await _groupService.AddUserToGroup(id, _userManager.GetUserId(User));
            return View(new GroupInviteModel {Group = group, UserId = userId});
        }

        [HttpPost]
        public async Task<IActionResult> GroupInvite(int groupId, string userId)
        {
            var res = await _groupService.AddUserToGroup(groupId, userId);
            var msg = new MessageViewModel()
            {
                ReturnUrl = "/Groups",
                Caption = res ? "Everything is ok!" : "Something went wrong!",
                Message = res
                    ? "You are added to group."
                    : "Internal error."
            };
            return View("Message", msg);
        }

        [HttpGet]
        public IActionResult SendInvite(int id)
        {
            return PartialView("_addUserToGroupDialog", id);
        }

        [HttpPost]
        public async Task<IActionResult> SendInvite(string email, int id)
        {
            await _emailSender.SendEmailAsync(email, "Group invite : Dumb Calendar",
                $"Your invite to group: <a href='localhost:5001/Groups/GroupInvite/{id}'>localhost:5001/Groups/GroupInvite/{id}</a>");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Participants(int id)
        {
            var group = await _groupService.GetById(id);
            return PartialView("_participantsList", group?.GroupParticipants);
        }

        public IActionResult AddGroup()
        {
            return PartialView("_addGroup", new BLL.DTO.GroupDTO());
        }

        [HttpPost]
        public async Task<IActionResult> AddGroup(GroupDTO group)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_addGroup", group);
            }

            await _groupService.AddNewGroup(group);
            return Content("Group added!");
        }

        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _groupService.GetById(id);
            string uid = _userManager.GetUserId(User);
            if (!group.CommandOwner.Equals(uid))
            {
                RedirectToAction("Index");
            }

            bool res = await _groupService.DeleteGroup(id);
            return View("Message", new MessageViewModel()
            {
                Caption = (res)
                    ? $"Group {group?.CommandName ?? "null"} deleted!"
                    : $"Group {group?.CommandName ?? "null"} not deleted!",
                Message = "",
                ReturnUrl = "/Groups"
            });
        }
    }
}