using MessagesApi.Domain.Models;
using MessagesApi.Domain.Repositories;
using MessagesApi.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        public async Task<ActionResult<IEnumerable<Message>>> ListAsync()
        {
            return await _messageRepository.ListAsync();
        }

        public async ValueTask<Message> FindAsync(long id)
        {
            return await _messageRepository.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _messageRepository.SaveChangesAsync();
        }

        public EntityEntry<Message> Add(Message message)
        {
            return _messageRepository.Add(message);
        }

        public EntityEntry<Message> Remove(Message message)
        {
            return _messageRepository.Add(message);
        }

        public bool MessageExists(long id)
        {
            return _messageRepository.MessageExists(id);
        }

        public void SetModifiedState(Message message)
        {
            _messageRepository.SetModifiedState(message);
        }
    }
}
