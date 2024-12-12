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
        private readonly IConfiguration _configuration;

        public GiftListsController(GiftListContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiftList>>> GetGiftLists([FromHeader(Name = "Authorization")] string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return Unauthorized("API key is required.");
            }
            
            if (apiKey != _configuration["ApiKey"])
            {
                return Unauthorized("Invalid API key.");
            }
            
            if (_context.GiftLists == null)
            {
                return NotFound(); 
            }
            var giftLists =  await _context.GiftLists.ToListAsync();
            return Ok(giftLists); 
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<GiftList>> GetGiftList(int id, [FromHeader(Name = "Authorization")] string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || apiKey != _configuration["ApiKey"])
            {
                return Unauthorized("Invalid API key.");
            }
            
            if (_context.GiftLists == null)
            {
                return NotFound();
            }
            
            var giftList = await _context.GiftLists.FindAsync(id);

            if (giftList == null)
            {
                return NotFound();
            }

            return Ok(giftList);
        }
        
        [HttpPost]
        public async Task<ActionResult<GiftList>> PostGiftList(GiftList giftList, [FromHeader(Name = "Authorization")] string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || apiKey != _configuration["ApiKey"])
            {
                return Unauthorized("Invalid API key.");
            }
            
            if (_context.GiftLists == null)
            {
                return Problem("Entity set 'GiftListContext.GiftLists' is null.");
            }
            
            _context.GiftLists.Add(giftList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGiftList", new { id = giftList.Id }, giftList); 
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGiftList(int id, [FromHeader(Name = "Authorization")] string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey) || apiKey != _configuration["ApiKey"])
            {
                return Unauthorized("Invalid API key.");
            }
            
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