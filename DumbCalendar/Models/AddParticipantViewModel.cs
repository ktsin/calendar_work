using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DumbCalendar.Models
{
    public class AddParticipantViewModel
    {
        public MultiSelectList SelectedUser { get; set; }

        public int TargetId { get; set; }

        public ICollection<BLL.DTO.UserDTO> AvailableUsers { get; set; }
    }
}