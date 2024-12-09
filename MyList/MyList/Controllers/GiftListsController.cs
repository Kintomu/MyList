using MyList.Data_Access;
using MyList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GiftListsController : ControllerBase
    {
        private readonly GiftListContext _context;

        public GiftListsController(GiftListContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftList>>> GetGiftLists()
        {
            if (_context.GiftLists == null)
            {
                return NotFound(); 
            }
            return await _context.GiftLists.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftList>> GetGiftList(int id)
        {
            if (_context.GiftLists == null)
            {
                return NotFound();
            }
            var giftList = await _context.GiftLists.FindAsync(id);

            if (giftList == null)
            {
                return NotFound();
            }

            return giftList;
        }
        
        [HttpPost]
        public async Task<ActionResult<GiftList>> PostGiftList(GiftList giftList)
        {
            if (_context.GiftLists == null)
            {
                return Problem("Entity set 'GiftListContext.GiftLists' is null.");
            }
            _context.GiftLists.Add(giftList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGiftList", new { id = giftList.Id }, giftList); 
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiftList(int id)
        {
            if (_context.GiftLists == null)
            {
                return NotFound();
            }
            var giftList = await _context.GiftLists.FindAsync(id);
            if (giftList == null)
            {
                return NotFound();
            }

            _context.GiftLists.Remove(giftList);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}