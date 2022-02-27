using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BLL.DTO
{
    public class TagDTO
    {
        [Key]
        public int Id { get; set; }
        
        public string OwnerId { get; set; }

        public KnownColor TagColor { get; set; } = KnownColor.Silver;
        
        public ICollection<CalendarEventDTO> CalendarEvents { get; set; }
        
        public ICollection<ProjectTaskDTO> ProjectTasks { get; set; }
    }
}
