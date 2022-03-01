using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMapper _mapper;
        private readonly IMessagesRepository _messagesRepository;
        private readonly IUserRepository _userRepository;

        public MessagesService(IMessagesRepository messagesRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _messagesRepository = messagesRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<UserDTO>> GetConversationsList(object userId)
        {
            var msgs = await _messagesRepository.GetBySelector(e => e.Sender.Equals(userId)
                                                                    || e.Recipient.Equals(userId));
            var conversationsWith = await Task.Run(() => msgs?
                .SelectMany(e => new string[] {e?.Recipient, e?.Sender})
                .Distinct()
                .Except(new[] {userId as string}).Select(e => _userRepository.GetById(e).Result)
                .Select(e => _mapper.Map<UserDTO>(e))
                .ToList());
            return conversationsWith;
        }

        public async Task<ICollection<MessageDTO>> GetConversationBetween(object userA, object userB)
        {
            try
            {
                var conversation = await _messagesRepository
                    .GetBySelector(e => e.Sender.Equals(userA) && e.Recipient.Equals(userB)
                                        || e.Sender.Equals(userB) && e.Recipient.Equals(userA));
                return await Task.Run(() => conversation?
                    .Select(e => _mapper.Map<MessageDTO>(e))
                    .ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<MessageDTO>();
            }
        }

        public async Task SendMessage(object recipient, object sender, string message)
        {
            await _messagesRepository.Create(
                new Message()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    MessageBody = message,
                    Recipient = recipient as string,
                    Sender = sender as string,
                    Sended = DateTime.Now
                });
        }
    }
}