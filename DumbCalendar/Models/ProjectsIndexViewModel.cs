using System.Collections.Generic;

namespace DumbCalendar.Models
{
    public class ProjectsIndexViewModel
    {
        public ICollection<BLL.DTO.ProjectDTO> UserOwn { get; set; }
        public ICollection<BLL.DTO.ProjectDTO> Participating { get; set; }
    }
}