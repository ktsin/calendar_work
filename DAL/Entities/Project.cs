using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public ICollection<User> Participants { get; set; }

        public ICollection<ProjectTask> Tasks { get; set; }
        
        public string ProjectOwner { get; set; }
        
        public string Name { get; set; }
        
        public DateTime ProjectStart { get; set; }
        
        public DateTime ProjectEnd { get; set; }
    }
}
