using Microsoft.AspNetCore.Mvc.RazorPages;
using MyGiftList.Data_Access;

namespace MyGiftList.Models;

public class IndexModel : PageModel
{
    private readonly GiftListContext _context;

    public IndexModel(GiftListContext context)
    {
        _context = context;
    }
    
    public List<GiftList> GiftLists { get; set; }

    public void OnGet()
    {
        GiftLists = _context.GiftLists.ToList();
    }

}