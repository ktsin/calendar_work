using System.Collections.Generic;

namespace DumbCalendar.Models
{
    public class ProjectTasksViewModel
    {
        public ICollection<BLL.DTO.ProjectTaskDTO> UserOwn { get; set; }
        public ICollection<BLL.DTO.ProjectTaskDTO> Participating { get; set; }
    }
}