using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupsRepository _groupsRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GroupService(IGroupsRepository groupsRepository,
            IMapper mapper,
            IUserRepository userRepository)
        {
            _groupsRepository = groupsRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<bool> AddUserToGroup(int groupId, string userId)
        {
            bool result = true;
            try
            {
                var user = await _userRepository.GetById(userId);
                var group = await _groupsRepository.GetById(groupId);
                group.GroupParticipants.Add(user);
                group = await _groupsRepository.Update(group);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        public async Task<ICollection<GroupDTO>> GetUserGroups(string userId)
        {
            var groups = await _groupsRepository.GetBySelector(e => e.CommandOwner.Equals(userId));
            return groups.Select(_mapper.Map<GroupDTO>).ToList();
        }

        public async Task<bool> DeleteGroup(int id)
        {
            return await _groupsRepository.Delete(id);
        }

        public async Task<GroupDTO> UpdateGroup(GroupDTO group)
        {
            var ngroup = await _groupsRepository.Update(_mapper.Map<Group>(group));
            return _mapper.Map<GroupDTO>(ngroup);
        }

        public async Task<bool> RemoveUserFromGroup(string userId, int groupId)
        {
            var user = await _userRepository.GetById(userId);
            var group = await _groupsRepository.GetById(groupId);
            group.GroupParticipants.Remove(user);
            group = await _groupsRepository.Update(group);
            return true;
        }

        public async Task<GroupDTO> AddNewGroup(GroupDTO group)
        {
            var ngroup = await _groupsRepository.Create(_mapper.Map<Group>(group));
            return _mapper.Map<GroupDTO>(group);
        }

        public async Task<GroupDTO> GetById(int id)
        {
            var group = await _groupsRepository.GetById(id);
            return _mapper.Map<GroupDTO>(group);
        }
    }
}