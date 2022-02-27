using System;
using System.Collections.Generic;

namespace BLL.Calendar
{
    public class Calendar
    {
        public DateTime Month { get; set; }

        public ICollection<CalendarDay> CalendarDays { get; set; }
    }
}