using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ProjectTask
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public int ProjectId { get; set; }
        
        public ICollection<User> Participants { get; set; }
        
        public string TaskName { get; set; }
        
        public ICollection<Tag> Tags { get; set; }
        
        public TaskPriority Priority { get; set; }
        
        public DateTime TaskStart { get; set; }
        
        public DateTime TaskEnd { get; set; }
        
    }
}
