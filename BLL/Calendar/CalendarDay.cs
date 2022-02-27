using System;
using System.Collections;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Calendar
{
    public class CalendarDay{
        
        public DateTime Day { get; set; }

        public ICollection<CalendarEventDTO> CalendarEvents { get; set; }

        public ICollection<ProjectTaskDTO> ProjectTasks { get; set; }
    }
}