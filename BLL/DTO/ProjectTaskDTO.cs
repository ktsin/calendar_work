using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.DTO
{
    public class ProjectTaskDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Project:")] public int ProjectId { get; set; }

        [Display(Name = "Participants:")] public ICollection<UserDTO> Participants { get; set; }

        [Display(Name = "Task name:")] public string TaskName { get; set; }

        [Display(Name = "Tags:")] public ICollection<TagDTO> Tags { get; set; }

        [Display(Name = "Task priority:")] public TaskPriority Priority { get; set; }

        [Display(Name = "Task start:")] public DateTime TaskStart { get; set; }

        [Display(Name = "End of task:")] public DateTime TaskEnd { get; set; }
    }
}