using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class UserDataService : IUserDataService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserDataService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDTO> AttachUserData(UserDTO value)
        {
            var user =  await _userRepository.Create(_mapper.Map<User>(value));
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> Update(UserDTO value)
        {
            var user =  await _userRepository.Update(_mapper.Map<User>(value));
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> DeleteUserData(object key)
        {
            var result =  await _userRepository.Delete(key);
            return result;
        }

        public async Task<ICollection<UserDTO>> ReadAll()
        {
            var users =  await _userRepository.ReadAll();
            return await Task.Run(() => users.Select(_mapper.Map<UserDTO>).ToList());
        }

        public async Task<ICollection<UserDTO>> ReadAllInclude()
        {
            var users =  await _userRepository.ReadAllInclude();
            return await Task.Run(() => users.Select(_mapper.Map<UserDTO>).ToList());
        }
        

        public async Task<UserDTO> GetUserDataById(object id)
        {
            var result =  await _userRepository.GetById(id);
            return _mapper.Map<UserDTO>(result);
        }
    }
}