using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessagesApi.Domain.Models;
using MessagesApi.Persistence.Contexts;
using MessagesApi.Domain.Services;

namespace MessagesApi.Controllers
{
    [Route("api/Messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly MessagesContext _context;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/MessagesItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _messageService.ListAsync();
            //return await _context.Messages.ToListAsync();
        }

        // GET: api/MessagesItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(long id)
        {
            var message = await _messageService.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/MessagesItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(long id, Message message)
        {
            if (id != message.Id)
            {
                return BadRequest();
            }

            var existingMessage = await _messageService.FindAsync(id);
            if (existingMessage == null)
            {
                return NotFound();
            }

            existingMessage.Text = message.Text;

            _messageService.SetModifiedState(message);

            try
            {
                await _messageService.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_messageService.MessageExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/MessagesItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage(Message message)
        {
            _messageService.Add(message);
            await _messageService.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessage), new { id = message.Id }, message);
        }

        // DELETE: api/MessagesItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessagesItem(long id)
        {
            var message = await _messageService.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _messageService.Remove(message);
            await _messageService.SaveChangesAsync();

            return NoContent();
        }
    }
}
