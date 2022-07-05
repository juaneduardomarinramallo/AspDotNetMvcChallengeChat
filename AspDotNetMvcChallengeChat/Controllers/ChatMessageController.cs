using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspDotNetMvcChallengeChat.Models;

namespace AspDotNetMvcChallengeChat.Controllers
{
    public class ChatMessageController : Controller
    {
        private readonly AspDotNetMvcChallengeChatDBContext _context;

        public ChatMessageController(AspDotNetMvcChallengeChatDBContext context)
        {
            _context = context;
        }

        // GET: ChatMessage
        public async Task<IActionResult> Index()
        {
            var aspDotNetMvcChallengeChatDBContext = _context.ChatMessages.Include(c => c.ChatRoom).Include(c => c.User);
            return View(await aspDotNetMvcChallengeChatDBContext.ToListAsync());
        }

        // GET: ChatMessage/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ChatMessages == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages
                .Include(c => c.ChatRoom)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            return View(chatMessage);
        }

        // GET: ChatMessage/Create
        public async Task<IActionResult> Create()
        {
            AspNetUser? aspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(u => u.UserName == User.Identity!.Name);
            ChatMessage chatMessage = new ChatMessage();

            if(aspNetUser != null)
                chatMessage.UserId = aspNetUser.Id;

            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "UserName");
            return View(chatMessage);
        }

        // POST: ChatMessage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ChatRoomId,Message")] ChatMessage chatMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatMessage.ChatRoomId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", chatMessage.UserId);

            
            return View(chatMessage);
        }

        // GET: ChatMessage/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ChatMessages == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage == null)
            {
                return NotFound();
            }
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatMessage.ChatRoomId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", chatMessage.UserId);
            return View(chatMessage);
        }

        // POST: ChatMessage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId,ChatRoomId,Message")] ChatMessage chatMessage)
        {
            if (id != chatMessage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatMessageExists(chatMessage.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChatRoomId"] = new SelectList(_context.ChatRooms, "Id", "Id", chatMessage.ChatRoomId);
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", chatMessage.UserId);
            return View(chatMessage);
        }

        // GET: ChatMessage/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ChatMessages == null)
            {
                return NotFound();
            }

            var chatMessage = await _context.ChatMessages
                .Include(c => c.ChatRoom)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatMessage == null)
            {
                return NotFound();
            }

            return View(chatMessage);
        }

        // POST: ChatMessage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ChatMessages == null)
            {
                return Problem("Entity set 'AspDotNetMvcChallengeChatDBContext.ChatMessages'  is null.");
            }
            var chatMessage = await _context.ChatMessages.FindAsync(id);
            if (chatMessage != null)
            {
                _context.ChatMessages.Remove(chatMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatMessageExists(long id)
        {
          return (_context.ChatMessages?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
