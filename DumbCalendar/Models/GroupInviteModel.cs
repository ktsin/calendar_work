using BLL.DTO;

namespace DumbCalendar.Models
{
    public class GroupInviteModel
    {
        public string UserId { get; set; }

        public GroupDTO Group { get; set; }
    }
}