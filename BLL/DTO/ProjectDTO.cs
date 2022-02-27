using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTO
{
    public class ProjectDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public ICollection<UserDTO> Participants { get; set; }

        public ICollection<ProjectTaskDTO> Tasks { get; set; }
        
        public string ProjectOwner { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ProjectStart { get; set; }
        
        public DateTime ProjectEnd { get; set; }
    }
}
