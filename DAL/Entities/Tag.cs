using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        
        public string OwnerId { get; set; }

        public KnownColor TagColor { get; set; } = KnownColor.Silver;
        
        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
