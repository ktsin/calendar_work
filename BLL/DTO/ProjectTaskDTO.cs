using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTO
{
    public class ProjectTaskDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        
        public ICollection<UserDTO> Participants { get; set; }
        
        public string TaskName { get; set; }
        
        public ICollection<TagDTO> Tags { get; set; }
        
        public TaskPriority Priority { get; set; }
        
        public DateTime TaskStart { get; set; }
        
        public DateTime TaskEnd { get; set; }
        
    }
}
