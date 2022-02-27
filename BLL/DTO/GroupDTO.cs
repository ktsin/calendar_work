using System.Collections.Generic;

namespace BLL.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }
        
        public int CommandOwner { get; set; }
        
        public string CommandName { get; set; }
        
        public ICollection<UserDTO> GroupParticipants { get; set; }
    }
}