namespace MyList.Models;

public class GiftList
{
    public int Id { get; set; }
    public string ListName { get; set; } = null!;
    public string GiftName { get; set; } = null!;
    public string GiftDescription { get; set; } = null!;
    public bool IsPurchased { get; set; }
}