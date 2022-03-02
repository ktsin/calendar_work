using System.Threading.Tasks;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DumbCalendar.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserDataService _userDataService;

        public UserController(IUserDataService userDataService)
        {
            _userDataService = userDataService;
        }

        public async Task<ViewResult> Details(string id)
        {
            var user = await _userDataService.GetUserDataById(id);
            return View(user);
        }
    }
}