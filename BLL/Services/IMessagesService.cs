using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services
{
    public interface IMessagesService
    {
        Task<ICollection<UserDTO>> GetConversationsList(object userId);
        Task<ICollection<MessageDTO>> GetConversationBetween(object userA, object userB);
        Task SendMessage(object recipient, object sender, string message);
    }
}