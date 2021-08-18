using MessagesApi.Domain.Models;
using MessagesApi.Domain.Repositories;
using MessagesApi.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Persistence.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(MessagesContext context) : base(context)
        {
        }

        public async Task<ActionResult<IEnumerable<Message>>> ListAsync()
        {
            return await _context.Messages.ToListAsync();
        }

        public async ValueTask<Message> FindAsync(long id)
        {
            return await _context.Messages.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public EntityEntry<Message> Add(Message message)
        {
            return _context.Messages.Add(message);
        }

        public EntityEntry<Message> Remove(Message message)
        {
            return _context.Messages.Remove(message);
        }

        public bool MessageExists(long id)
        {
            return _context.Messages.Any(m => m.Id == id);
        }

        public void SetModifiedState(Message message) 
        {  
            _context.Entry(message).State = EntityState.Modified; 
        }
    }
}
