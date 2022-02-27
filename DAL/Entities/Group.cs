using System.Collections;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Group
    {
        public int Id { get; set; }
        
        public string CommandOwner { get; set; }
        
        public string CommandName { get; set; }
        
        public ICollection<User> GroupParticipants { get; set; }
    }
}