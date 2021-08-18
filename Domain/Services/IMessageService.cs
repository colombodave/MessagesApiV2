using MessagesApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesApi.Domain.Services
{
    public interface IMessageService
    {
        Task<ActionResult<IEnumerable<Message>>> ListAsync();
        ValueTask<Message> FindAsync(long id);
        Task<int> SaveChangesAsync();
        EntityEntry<Message> Add(Message message);
        EntityEntry<Message> Remove(Message message);
        bool MessageExists(long id);
        void SetModifiedState(Message message);
    }
}
