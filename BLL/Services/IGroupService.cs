using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services
{
    public interface IGroupService
    {
        Task<bool> AddUserToGroup(int groupId, string userId);
        Task<ICollection<GroupDTO>> GetUserGroups(string userId);
        Task<bool> DeleteGroup(int id);
        Task<GroupDTO> UpdateGroup(GroupDTO group);
        Task<bool> RemoveUserFromGroup(string userId, int groupId);
        Task<GroupDTO> AddNewGroup(GroupDTO group);
        Task<GroupDTO> GetById(int id);
    }
}