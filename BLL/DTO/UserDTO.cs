using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class UserDTO
    {
        [Key]
        public string Id { get; set; }
        
        public string FullName { get; set; }
        
        [DataType(DataType.Text)]
        public DateTime Birthday { get; set; }
        
        public ICollection<GroupDTO> Groups { get; set; }
        
        public ICollection<ProjectDTO> InProjects { get; set; }
        
        public ICollection<ProjectDTO> UserProjects { get; set; }
        
        public ICollection<ProjectTaskDTO> Tasks { get; set; }
        
    }
}
