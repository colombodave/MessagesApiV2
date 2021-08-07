using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessagesApi.Models;

namespace MessagesApi.Controllers
{
    [Route("api/MessagesItems")]
    [ApiController]
    public class MessagesItemsController : ControllerBase
    {
        private readonly MessagesContext _context;

        public MessagesItemsController(MessagesContext context)
        {
            _context = context;
        }

        // GET: api/MessagesItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessagesItemDTO>>> GetMessagesItems()
        {
            return await _context.MessagesItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/MessagesItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessagesItemDTO>> GetMessagesItem(long id)
        {
            var messagesItem = await _context.MessagesItems.FindAsync(id);

            if (messagesItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(messagesItem);
        }

        // PUT: api/MessagesItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessagesItem(long id, MessagesItemDTO messagesItemDTO)
        {
            if (id != messagesItemDTO.Id)
            {
                return BadRequest();
            }

            var messagesItem = await _context.MessagesItems.FindAsync(id);
            if (messagesItem == null)
            {
                return NotFound();
            }

            messagesItem.Message = messagesItemDTO.Message;

            //_context.Entry(messagesItemDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MessagesItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/MessagesItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessagesItemDTO>> CreateMessagesItem(MessagesItemDTO messagesItemDTO)
        {
            var messagesItem = new MessagesItem
            {
                Message = messagesItemDTO.Message
            };

            _context.MessagesItems.Add(messagesItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMessagesItem), new { id = messagesItem.Id }, ItemToDTO(messagesItem));
        }

        // DELETE: api/MessagesItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessagesItem(long id)
        {
            var messagesItem = await _context.MessagesItems.FindAsync(id);
            if (messagesItem == null)
            {
                return NotFound();
            }

            _context.MessagesItems.Remove(messagesItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessagesItemExists(long id)
        {
            return _context.MessagesItems.Any(e => e.Id == id);
        }

        private static MessagesItemDTO ItemToDTO(MessagesItem messagesItem) => new MessagesItemDTO
        {
            Id = messagesItem.Id,
            Message = messagesItem.Message
        };
    }
}
