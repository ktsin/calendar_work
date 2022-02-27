using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class CalendarEventDTO
    {
        [Key] public string Id { get; set; }

        public string OwnerId { get; set; }

        [Display(Name = "Date and time of event")]
        public DateTime EventDate { get; set; }

        [Range(typeof(TimeSpan), "00:01:00", "23:59:59")]
        [Display(Name = "Event duration")]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Note")] public string Comment { get; set; }

        public ICollection<TagDTO> Tags { get; set; }
    }
}