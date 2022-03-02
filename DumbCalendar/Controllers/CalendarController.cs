using System;
using System.Threading.Tasks;
using BLL.Calendar;
using BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DumbCalendar.Controllers
{
    [Authorize]
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly UserManager<IdentityUser> _userManager;

        public CalendarController(ICalendarService calendarService,
            UserManager<IdentityUser> userManager)
        {
            _calendarService = calendarService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            return View(await _calendarService.GetCalendarForUser(userId, DateTime.Now));
        }

        [Authorize]
        public async Task<IActionResult> IndexSpecified(DateTime month)
        {
            var userId = _userManager.GetUserId(User);
            return View("Index", await _calendarService.GetCalendarForUser(userId,
                CalendarUtils.GetStartOfMonth((Month) month.Month, month.Year)));
        }

        [Authorize]
        public async Task<IActionResult> DayDetailed(DateTime day)
        {
            var userId = _userManager.GetUserId(User);
            var calendarDay = await _calendarService.GetCalendarDay(userId, day);
            return View(calendarDay);
        }

        [Authorize]
        public IActionResult AddCalendarEvent()
        {
            return View(new CalendarEventDTO());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddCalendarEvent(CalendarEventDTO value)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Wrong input!");
                return View(value);
            }

            await _calendarService.AddCalemdarEvent(value);
            return RedirectToAction("DayDetailed", new {day = value.EventDate});
        }
    }
}