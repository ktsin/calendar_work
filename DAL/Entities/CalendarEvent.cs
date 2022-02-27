using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CalendarEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public DateTime EventDate { get; set; }

        public TimeSpan Duration { get; set; }

        public string Comment { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}