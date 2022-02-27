using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Services
{
    public interface IUserDataService
    {
        Task<UserDTO> AttachUserData(UserDTO value);
        Task<UserDTO> Update(UserDTO value);
        Task<bool> DeleteUserData(object key);
        Task<ICollection<UserDTO>> ReadAll();
        Task<ICollection<UserDTO>> ReadAllInclude();
        Task<UserDTO> GetUserDataById(object id);
    }
}