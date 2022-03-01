using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class GroupDTO
    {
        public int Id { get; set; }

        [Display(Name = "Command owner")] public string CommandOwner { get; set; }

        [Display(Name = "Command name")] public string CommandName { get; set; }

        public ICollection<UserDTO> GroupParticipants { get; set; }
    }
}