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
    public class ChatRoomController : Controller
    {
        private readonly AspDotNetMvcChallengeChatDBContext _context;

        public ChatRoomController(AspDotNetMvcChallengeChatDBContext context)
        {
            _context = context;
        }

        // GET: ChatRoom
        public async Task<IActionResult> Index()
        {
              return _context.ChatRooms != null ? 
                          View(await _context.ChatRooms.ToListAsync()) :
                          Problem("Entity set 'AspDotNetMvcChallengeChatDBContext.ChatRooms'  is null.");
        }

        // GET: ChatRoom/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ChatRooms == null)
            {
                return NotFound();
            }

            var chatRoom = await _context.ChatRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            return View(chatRoom);
        }

        // GET: ChatRoom/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChatRoom/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ChatRoom chatRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatRoom);
        }

        // GET: ChatRoom/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ChatRooms == null)
            {
                return NotFound();
            }

            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom == null)
            {
                return NotFound();
            }
            return View(chatRoom);
        }

        // POST: ChatRoom/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ChatRoom chatRoom)
        {
            if (id != chatRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatRoomExists(chatRoom.Id))
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
            return View(chatRoom);
        }

        // GET: ChatRoom/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ChatRooms == null)
            {
                return NotFound();
            }

            var chatRoom = await _context.ChatRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatRoom == null)
            {
                return NotFound();
            }

            return View(chatRoom);
        }

        // POST: ChatRoom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ChatRooms == null)
            {
                return Problem("Entity set 'AspDotNetMvcChallengeChatDBContext.ChatRooms'  is null.");
            }
            var chatRoom = await _context.ChatRooms.FindAsync(id);
            if (chatRoom != null)
            {
                _context.ChatRooms.Remove(chatRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatRoomExists(int id)
        {
          return (_context.ChatRooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
