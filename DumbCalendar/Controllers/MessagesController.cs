using System;
using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DumbCalendar.Controllers
{
    // public class MessagesController : Controller
    // {
    //     // GET
    //     public IActionResult Index()
    //     {
    //         return View();
    //     }
    //
    //     public IActionResult Details()
    //     {
    //         throw new System.NotImplementedException();
    //     }
    //
    //     public async Task<IActionResult> Send(MessageDTO)
    //     {
    //         
    //     }
    // }
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessagesService _messageService;
        private readonly UserManager<IdentityUser> _user;

        public MessagesController(IMessagesService messageService, UserManager<IdentityUser> user)
        {
            _messageService = messageService;
            _user = user;
        }

        public async Task<IActionResult> Index()
        {
            var id = _user.GetUserId(User);
            return View("Index", await _messageService.GetConversationsList(id));
        }

        public async Task<IActionResult> Details(string id)
        {
            ViewBag.Recipient = id;
            var uid = _user.GetUserId(User);
            var conversations = await _messageService.GetConversationBetween(uid, id);
            return View("Details", conversations);
        }

        [HttpPost]
        public async Task<IActionResult> Send(string messageBody, string sender, string recipient)
        {
            var res = new ContentResult {Content = "Message sent!"};
            if (messageBody?.Length == 0)
                res.Content = "Message must be > 0 symbols!";
            else
            {
                try
                {
                    await _messageService.SendMessage(sender, recipient, messageBody);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    res.Content = "Exception occures, try again later!";
                }
            }

            return res;
        }
    }
}